using UnityEngine;
using System.Collections;


namespace Kalagaan
{
    public class DragonFireController : MonoBehaviour
    {

        [System.Serializable]
        public class ParticlesData
        {
            public ParticleSystem ps;
            public float maxSpeed = 200f;
            public float maxSize = 200f;
        }

        public float m_intensity = 0f;
        public ParticlesData[] m_particlesData;


        // Use this for initialization
        void Start()
        {
        }




        // Update is called once per frame
        void Update()
        {            
            for( int i=0; i< m_particlesData.Length; ++i )
            {
                if (m_intensity > 0)
                {
                    if (m_particlesData[i].ps.isStopped)
                        m_particlesData[i].ps.Play();

                    var speed = m_particlesData[i].ps.main;
                    speed.startSpeed = m_particlesData[i].maxSpeed * m_intensity * m_intensity * transform.lossyScale.magnitude ;

                    var size = m_particlesData[i].ps.main;
                    size.startSize = m_particlesData[i].maxSize * m_intensity * m_intensity * transform.lossyScale.magnitude;
                }
                else
                {
                    m_particlesData[i].ps.Stop();
                }
            }
        }
    }
}
