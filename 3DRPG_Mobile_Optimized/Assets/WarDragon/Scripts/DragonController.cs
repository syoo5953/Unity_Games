using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;


namespace Kalagaan
{
    //[ExecuteInEditMode]
    public class DragonController : CharacterBase
    {
        [System.Serializable]
        public class IKData
        {
            public float weight = 1f;
            public float boneAngleLimit = 30f;
            public Transform startBone;
            public Transform endBone;
            public float speed = 3f;
            [HideInInspector]
            public Quaternion[] buffer;
            [HideInInspector]
            public Quaternion[] lastComputed;
            [HideInInspector]
            public List<Transform> bones = new List<Transform>();
            public Vector3 lookAtFix = new Vector3(90f, 90f, 0f);//blender to unity bone rotation fix

            public void Init()
            {
                bones.Clear();
                bones.Add(startBone);
                Transform parent = startBone.parent;
                while (parent != endBone && parent != null)
                {
                    bones.Add(parent);
                    parent = parent.parent;
                }
                if (parent == endBone)
                {
                    bones.Add(endBone);
                    buffer = new Quaternion[bones.Count];
                    lastComputed = new Quaternion[bones.Count];
                }
                else
                    bones.Clear();
            }

        }






        //Vector3 m_lookAtFix = new Vector3(90f, 90f, 0f);//blender to unity bone rotation fix



        public IKData m_headLook = new IKData();
        public IKData m_tailLook = new IKData();
        public SkinnedMeshRenderer m_dragonMesh;
        public SkinnedMeshRenderer m_tongMesh;

        public Transform m_jaw;
        public float m_openJaw = 0f;
        public Vector2 m_jawLimits = new Vector2(-3f, 70f);

        public float m_closeEyes = 0f;
        public float m_blinkRate = 1f;
        float m_lastBlinkTime = 0;
        bool m_blink;
        float m_blinkWeight = 0;

        public DragonFireController m_firePrefab;
        DragonFireController m_fire;
        public Vector3 m_fireOffset = new Vector3(0, -1, 0);

        public float m_fireIntensity = 0f;
        //Vector3 m_lastHeadposition;

        float m_fireEmission = 0f;

        Light m_light;

        Animator m_animator;

        /*
        public PlayerBase playerBase;
        PlayerBase target;
        */

        //mine
        public Transform target;
        public GameObject cubeBase;
        private CharacterController ct;
        private bool isLanding = false;
        private float dist;
        private float SkillDelay;
        private bool isFireReady;
        public float skill_rate;
        private bool isSkill;
        public float distToGround;
        public NavMeshAgent nav;
        private bool stopRotating = true;
        float degree = 0;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            ct = GetComponent<CharacterController>();
            m_animator = GetComponent<Animator>();
        }

        void Start()
        {

            m_headLook.Init();
            m_tailLook.Init();

            m_fire = Instantiate(m_firePrefab);

            m_light = m_headLook.bones[0].gameObject.AddComponent<Light>();
            m_light.type = LightType.Point;
            m_light.intensity = 0;
            m_light.range = .4f * transform.lossyScale.magnitude;

        }   

        private void Update()
        {
            if(!stopRotating)
            {
                Rotate();
            }

            if (isLanding)
            {
                Land();
            }

            /*
            SkillDelay += Time.deltaTime;
            dist = Vector3.Distance(transform.position, target.transform.position);
            Debug.Log(dist);

            if (dist < 15)
            {
                FollowPlayer();
            }

            if (isLanding)
            {
                Land();
            }
            FireBreath();
            */
        }

        private void Rotate()
        {
            //SoundManager.Instance.playAudio("DragonFly");
            m_animator.SetBool("Fly", true);
            if (transform.position.y < 25f)
            {
                //var rotation = Quaternion.LookRotation(new Vector3(-22f, 178, 0) - transform.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
                transform.position += new Vector3(0, 0.2f, 0);
            }
            else
            {
                transform.RotateAround(Vector3.zero, Vector3.up, 80 * Time.deltaTime);
                degree += 80 * Time.deltaTime;
            };

            if (degree >= 360)
            {
                m_animator.SetBool("Fly", false);
                isLanding = true;
                stopRotating = true;
                degree = 0;
            }
        }


        private void FollowPlayer()
        {
             nav.SetDestination(target.transform.position);
             m_animator.SetBool("Walk", true);
        }
        private void Land()
        {
            StartCoroutine(Roar());
            m_animator.SetBool("Land", true);
            transform.position += new Vector3(0, -0.2f, 0);
            if(Physics.Raycast(transform.position, Vector3.down, distToGround))
            {
                Debug.Log("drag grounded");
                m_animator.SetBool("Land", false);
                isLanding = false;
            }
        }

        IEnumerator Roar()
        {
            yield return new WaitForSeconds(2.5f);
            SoundManager.Instance.playAudio("DragonRoar");
        }

        private void FireBreath()
        {
            isFireReady = skill_rate < SkillDelay;
            if (isFireReady)
            {
                if (dist > 30 && dist < 40)
                {
                    nav.velocity = Vector3.zero;
                    var rotation = Quaternion.LookRotation(cubeBase.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
                    m_fireIntensity = 1;
                    StartCoroutine(BreathOff());
                }
            }
        }

        IEnumerator BreathOff()
        {
            SkillDelay = 0;
            yield return new WaitForSeconds(3f);
            m_fireIntensity = 0;
        }

        void LateUpdate()
        {
            //Look at
            ComputeKinematic(m_headLook);
            
            //tail
            ComputeKinematic(m_tailLook);


            //jaw
            m_openJaw = Mathf.Clamp01(m_openJaw);
            m_fireIntensity = Mathf.Clamp01(m_fireIntensity);
            float openJaw = Mathf.Max(m_openJaw, m_fireIntensity);
            if (m_jaw != null)
            {

                if(openJaw > 0 )
                    m_jaw.localRotation = Quaternion.Euler(-180f, Mathf.Lerp(m_jawLimits.x, m_jawLimits.y, openJaw), 0f);

                //tong                    
                float fullAngle = Quaternion.Angle(Quaternion.Euler(-180f, m_jawLimits.x, 0f), Quaternion.Euler(-180f, m_jawLimits.y, 0f));
                float openAngle = Quaternion.Angle(m_jaw.localRotation, Quaternion.Euler(-180f, m_jawLimits.x, 0f));
                m_tongMesh.SetBlendShapeWeight(0, (openAngle/ fullAngle) * 100f * (1f-m_fireIntensity) );

            }

            
                


            //eyes
            m_closeEyes = Mathf.Clamp01(m_closeEyes);
            if (m_closeEyes == 0f)
            {

                if (Time.time - m_lastBlinkTime > m_blinkRate * .8f + m_blinkRate * .2f * Random.value)
                {
                    m_blink = true;
                }

                if (m_blink)
                {
                    m_blinkWeight += Time.deltaTime * 8f;
                    m_blinkWeight = Mathf.Clamp01(m_blinkWeight);
                    if (m_blinkWeight == 1f)
                    {
                        m_blink = false;
                        m_lastBlinkTime = Time.time;
                    }
                }
                else
                {
                    m_blinkWeight -= Time.deltaTime * 3f;
                    m_blinkWeight = Mathf.Clamp01(m_blinkWeight);
                }

                m_dragonMesh.SetBlendShapeWeight(0, m_blinkWeight * 100f);
            }
            else
            {
                m_dragonMesh.SetBlendShapeWeight(0, m_closeEyes * 100f);
            }


            m_fire.transform.position = m_headLook.bones[0].position;
            m_fire.transform.rotation = m_headLook.bones[0].rotation *Quaternion.Euler(m_headLook.lookAtFix.x + 90f, m_headLook.lookAtFix.y, 0f);
            m_headLook.bones[0].localRotation *= Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0f, -30f, 0f), openJaw);


            m_fireEmission = Mathf.Lerp(m_fireEmission, m_fireIntensity, Time.deltaTime * .3f);

            //m_dragonMesh.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            m_dragonMesh.material.SetColor("_EmissionColor", Color.Lerp( Color.black, Color.white, m_fireEmission) );
            m_tongMesh.material = m_dragonMesh.material;
            //DynamicGI.UpdateMaterials(m_dragonMesh);
            //DynamicGI.UpdateMaterials(m_tongMesh);
            //DynamicGI.UpdateEnvironment();

            m_light.intensity = 8f* m_fireIntensity;

            m_fire.m_intensity = m_fireIntensity;
            m_fire.transform.localScale = transform.localScale;

        }



        public void ComputeInverseKinematic(List<Transform> bones, Vector3 target)
        {

        }

        public void ComputeKinematic(IKData ik )
        {
            ik.weight = Mathf.Clamp01(ik.weight);           
            ComputeKinematic(ik.bones,ik.buffer, ik.boneAngleLimit, target.transform, ik.weight, ik.lastComputed, ik.speed, ik.lookAtFix);
        }


        public void ComputeKinematic(List<Transform> bones, Quaternion[] buffer, float angleLimit, Transform target, float weight, Quaternion[] m_lastComputed, float speed, Vector3 lookAtFix)
        {
            for (int i = 0; i < bones.Count; ++i)
                buffer[i] = bones[i].localRotation;

            if( m_lastComputed.Length != buffer.Length )
                for (int i = 0; i < bones.Count; ++i)
                    m_lastComputed[i] = buffer[i];


            if (target != null && weight > 0)
            {
                for (int n = 0; n < 10; ++n)//iteration
                {
                    float parentDot = 1f;
                    for (int i = 0; i < bones.Count; ++i)
                    //for (int i = bones.Count-1; i >= 0; --i)
                    {
                        Quaternion qWorld = bones[i].rotation;
                        float f = (float)(i + 1) / (float)(bones.Count + 1);
                        f *= f * f;

                        //bones[i].LookAt(target, transform.up);
                        bones[i].LookAt(target);
                        bones[i].rotation *= Quaternion.Euler(lookAtFix);
                        Quaternion perfectRotation = bones[i].rotation;

                        if (i != 0)
                            bones[i].rotation = Quaternion.Lerp(qWorld, bones[i].rotation, 1f - parentDot);

                        bones[i].localRotation = LimitAngles(bones[i].localRotation, angleLimit);

                        parentDot = Quaternion.Dot(perfectRotation, bones[i].rotation);
                        parentDot = Mathf.Clamp01(parentDot);
                    }
                }


                for (int i = 0; i < bones.Count; ++i)
                    bones[i].localRotation = Quaternion.Lerp(buffer[i], bones[i].localRotation, weight);

            }
            for (int i = 0; i < bones.Count; ++i)
            {
                bones[i].localRotation = Quaternion.Lerp(m_lastComputed[i], bones[i].localRotation, Time.deltaTime * speed);
                m_lastComputed[i] = bones[i].localRotation;
            }


        }




        public Quaternion LimitAngles(Quaternion q, float angle)
        {
            Vector3 euler = q.eulerAngles;
            if (euler.x > 180) euler.x -= 360f;
            if (euler.y > 180) euler.y -= 360f;
            if (euler.z > 180) euler.z -= 360f;
            euler.x = Mathf.Clamp(euler.x, -angle, angle);
            euler.y = Mathf.Clamp(euler.y, -angle, angle);
            euler.z = Mathf.Clamp(euler.z, -angle, angle);
            return Quaternion.Euler(euler);
        }



        //----------------------------------------------------
        //UI interactions


        public void SetCloseEyes(UnityEngine.UI.Slider slider)
        {
            m_closeEyes = slider.value;
        }

        public void SetOpenJaw(UnityEngine.UI.Slider slider )
        {
            m_openJaw = slider.value;
        }

        public void SetFireIntensity( UnityEngine.UI.Slider slider)
        {
            m_fireIntensity = slider.value;
        }

        public void SetHeadLookAtWeight(UnityEngine.UI.Slider slider)
        {
            m_headLook.weight = slider.value;
        }

        public void TurnOnNav()
        {
            nav.enabled = true;
        }
    }
}
