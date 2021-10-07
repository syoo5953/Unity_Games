using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float turnSpeed = 20f;
    public int jumpPower;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public int ammo;
    public Camera followCamera;
    public int health;

    float hAxis;
    float vAxis;
    float fireDelay;
    float speed;

    bool jDown;
    bool dDown;
    bool isJump;
    bool isDodge;
    bool isSwap;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool fDown;
    bool rDown;
    bool isFireReady = true;
    bool isBoarder;
    bool isReload;
    bool isDamage;

    Rigidbody m_Rigidbody;
    Vector3 moveVec;
    Animator m_Animator;
    Quaternion m_Rotation;

    GameObject nearObject;
    Weapon equipWeapon;
    MeshRenderer[] meshs;

    AudioSource audioSource;

    int equipWeaponIndex = -1;
    void Awake()
    {
        m_Rotation = Quaternion.identity;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }
    void FixedUpdate()
    {
        FreezeRotation(); //물리적 collision이 일어났을때 도는 현상 고치기. 그냥 Constraint에서 Freeze Rotation Y 체크해줘도 해결됨
        StoptoWall();
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        PickupWeapon();
        Swap();
        Attack();
        Reload();
        /*
        Ray ray = new Ray(transform.position, Vector3.down);

        //will store info of successful ray cast
        RaycastHit hitInfo;

        //terrain should have mesh collider and be on custom terrain 
        //layer so we don't hit other objects with our raycast
        LayerMask layer = 1 << LayerMask.NameToLayer("Terrain");

        //cast ray
        if (Physics.Raycast(ray, out hitInfo, layer))
        {
            //get where on the z axis our raycast hit the ground
            float z = hitInfo.point.z;

            //copy current position into temporary container
            Vector3 pos = transform.position;

            //change z to where on the z axis our raycast hit the ground
            pos.z = z;

            //override our position with the new adjusted position.
            transform.position = pos;
        }*/
        }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        dDown = Input.GetButtonDown("Dodge");
        iDown = Input.GetButtonDown("PickupWeapon");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        fDown = Input.GetButtonDown("Fire1");
        rDown = Input.GetButtonDown("Reload");
    }

    void Move()
    {
        if (!isDodge) moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isSwap || !isFireReady) moveVec = Vector3.zero;

        if(!isBoarder)
        transform.position += moveVec * speed * Time.deltaTime;
        m_Animator.SetBool("isRun", moveVec != Vector3.zero);

    }

    void Turn()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, moveVec, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);

        
        //마우스 회전
        
        if(fDown && !isReload)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    void Jump()
    {
        if (jDown && !isJump && !isDodge && !isSwap && !isReload)
        {
            audioSource.Play();
            m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            m_Animator.SetBool("isJump", true);
            m_Animator.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Attack()
    {
        if(equipWeapon == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady && !isDodge && !isSwap && !isReload)
        {
                if (equipWeapon.type == Weapon.Type.Hammer)
                {
                    m_Animator.SetFloat("speed", 0.6f);
                } else if(equipWeapon.type == Weapon.Type.Sword)
                {
                    m_Animator.SetFloat("speed", 2);
                }
                equipWeapon.Use();
                m_Animator.SetTrigger(equipWeapon.tag == "Melee" ? "doSwing" : "doShot");
                fireDelay = 0;
        }
    }

    void Reload()
    {
        if(equipWeapon == null) return;

        if (equipWeapon.tag == "Melee") return;

        if (ammo == 0) return;

        if(rDown && !isReload && !isJump && !isDodge && !isSwap && isFireReady)
        {
            m_Animator.SetTrigger("doReload");
            isReload = true;

            Invoke("ReloadOut", 3f);
        }
    }

    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo;
        ammo -= reAmmo;
        isReload = false;
    }

    void Dodge()
    {
        if (dDown && !isDodge && !isJump && !isDodge && !isSwap && !isReload)
        {
            m_Animator.SetTrigger("doDodge");
            speed *= 2;
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut() {
        speed *= 0.5f;
        isDodge = false;
    }

    void Swap()
    {
        if(sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0 ))
        {
            return;
        }
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
        {
            return;
        }
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
        {
            return;
        }


        int weaponIndex = -1;

        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3) && !isJump && !isDodge)
        {
            if(equipWeapon != null)
            equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            //equipWeapon.gameObject.SetActive(true);
            weapons[equipWeaponIndex].SetActive(true);
            m_Animator.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void PickupWeapon()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
            }
        }
    }

    void FreezeRotation()
    {
        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    void StoptoWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        isBoarder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            m_Animator.SetBool("isJump", false);
            m_Animator.SetTrigger("doJump");
            isJump = false;
        }
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + moveVec * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            if(!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;

                bool isBossAtk = other.name == "Boss Melee Area";
                Debug.Log(isBossAtk);
                StartCoroutine(OnDamage(isBossAtk));
            }
            if (other.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator OnDamage(bool isBossAtk)
    {
        isDamage = true;

        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        if(isBossAtk)
        {
            m_Rigidbody.AddForce(transform.forward * -350, ForceMode.Impulse);
        }
        
        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

        if(isBossAtk)
        {
            m_Rigidbody.velocity = Vector3.zero;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon" || other.tag == "Shop")
        {
            nearObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = null;
        } else if(other.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
    }
}
