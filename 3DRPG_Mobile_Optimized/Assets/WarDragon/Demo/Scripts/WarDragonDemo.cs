using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Kalagaan
{
    public class WarDragonDemo : MonoBehaviour
    {
        public Animator m_dragon;
        public GroundController m_ground;
        
        public Button m_flyBtn;
        public Button m_walkBtn;
        public Button m_runBtn;
        public Button m_Attack1Btn;
        public Button m_Attack2Btn;
        public Button m_SleepBtn;
        public Button m_WakeUpBtn;
        public Button m_idleBtn;
        public Button m_idle2Btn;
        public Button m_landBtn;
        public Button m_stationaryBtn;
        public Button m_planeBtn;
        public Button m_dieBtn;
        public Button m_respawn;
        public Button m_taunt;
        public Button m_hit;


        void Awake()
        {
            m_dragon.SetBool("Fly", false);
            m_dragon.SetBool("Fly_plane", false);
            m_dragon.SetBool("Fly_stationary", false);
            m_dragon.SetBool("Idle2", false);
            m_dragon.SetBool("Sleep", false);
            m_dragon.SetBool("Taunt", false);
            m_dragon.SetBool("Attack1", false);
            m_dragon.SetBool("Attack2", false);
            m_dragon.SetBool("Hit 1", false);
            m_dragon.SetBool("Hit 2", false);
            m_dragon.SetBool("Walk", false);
            m_dragon.SetBool("Run", false);
            m_dragon.SetBool("Die", false);


            m_flyBtn.onClick.AddListener( Fly );
            m_walkBtn.onClick.AddListener(Walk);
            m_runBtn.onClick.AddListener(Run);
            m_Attack1Btn.onClick.AddListener(Attack1);
            m_Attack2Btn.onClick.AddListener(Attack2);
            m_SleepBtn.onClick.AddListener(Sleep);
            m_WakeUpBtn.onClick.AddListener(WakeUp);
            m_idleBtn.onClick.AddListener(Idle1);
            m_idle2Btn.onClick.AddListener(Idle2);
            m_landBtn.onClick.AddListener(Land);
            m_stationaryBtn.onClick.AddListener(Stationary);
            m_planeBtn.onClick.AddListener(Plane);
            m_dieBtn.onClick.AddListener(Die);
            m_respawn.onClick.AddListener(Respawn);
            m_taunt.onClick.AddListener(Taunt);
            m_hit.onClick.AddListener(Hit);

            ShowFlyModeButtons(false);
        }



        public void ShowFlyModeButtons( bool flyMode )
        {
            SetAllButtonActive(false);

            if (flyMode)
            {
                m_flyBtn.gameObject.SetActive(true);
                m_landBtn.gameObject.SetActive(true);
                m_stationaryBtn.gameObject.SetActive(true);
                m_planeBtn.gameObject.SetActive(true);
                m_dieBtn.gameObject.SetActive(true);
            }
            else
            {
                m_flyBtn.gameObject.SetActive(true);
                m_walkBtn.gameObject.SetActive(true);
                m_runBtn.gameObject.SetActive(true);
                m_Attack1Btn.gameObject.SetActive(true);
                m_Attack2Btn.gameObject.SetActive(true);
                m_SleepBtn.gameObject.SetActive(true);
                m_idle2Btn.gameObject.SetActive(true);
                //m_idleBtn.gameObject.SetActive(true);
                m_taunt.gameObject.SetActive(true);
                m_hit.gameObject.SetActive(true);
                m_dieBtn.gameObject.SetActive(true);
            }
        }
        

        public void Fly()
        {
            m_dragon.SetBool("Fly", true);
            m_dragon.SetBool("Fly_plane", false);
            m_dragon.SetBool("Fly_stationary", false);
            m_dragon.SetBool("Idle2", false);

            StartCoroutine(ChangeNextFrame("Run", false));
            StartCoroutine(ChangeNextFrame("Walk", false));

            m_ground.SetFlyMode(true);
            ShowFlyModeButtons(true);
            m_flyBtn.gameObject.SetActive(false);
            
        }


        public void Land()
        {
            m_dragon.SetBool("Fly", false);
            m_dragon.SetBool("Fly_plane", false);
            m_dragon.SetBool("Fly_stationary", false);
            
            m_ground.SetFlyMode(false);
            ShowFlyModeButtons(false);
            
        }


        public void Walk()
        {
            m_dragon.SetBool("Walk", true);
            m_dragon.SetBool("Run", false);
            m_walkBtn.gameObject.SetActive(false);
            m_runBtn.gameObject.SetActive(true);
            m_idleBtn.gameObject.SetActive(true);
            m_idle2Btn.gameObject.SetActive(false);
            m_Attack1Btn.gameObject.SetActive(false);
            m_Attack2Btn.gameObject.SetActive(false);
            m_SleepBtn.gameObject.SetActive(false);
            m_taunt.gameObject.SetActive(false);
            m_hit.gameObject.SetActive(false);
            m_dieBtn.gameObject.SetActive(false);
        }


        public void Run()
        {
            m_dragon.SetBool("Run", true);
            m_walkBtn.gameObject.SetActive(true);
            m_runBtn.gameObject.SetActive(false);
            m_idleBtn.gameObject.SetActive(true);
            m_idle2Btn.gameObject.SetActive(false);
            m_Attack1Btn.gameObject.SetActive(false);
            m_Attack2Btn.gameObject.SetActive(false);
            m_SleepBtn.gameObject.SetActive(false);
            m_taunt.gameObject.SetActive(false);
            m_hit.gameObject.SetActive(false);
            m_dieBtn.gameObject.SetActive(false);
        }


        public void Attack1()
        {
            m_dragon.SetBool("Attack1", true);
            StartCoroutine(ChangeNextFrame("Attack1",false));
        }

        public void Attack2()
        {
            m_dragon.SetBool("Attack2", true);
            StartCoroutine(ChangeNextFrame("Attack2", false));
        }

        public void Idle1()
        {
            m_dragon.SetBool("Walk", false);
            m_dragon.SetBool("Run", false);
            m_dragon.SetBool("Idle2", false);
            m_idleBtn.gameObject.SetActive(false);
            m_idle2Btn.gameObject.SetActive(true);
            m_walkBtn.gameObject.SetActive(true);
            m_runBtn.gameObject.SetActive(true);
            m_Attack1Btn.gameObject.SetActive(true);
            m_Attack2Btn.gameObject.SetActive(true);
            m_SleepBtn.gameObject.SetActive(true);
            m_taunt.gameObject.SetActive(true);
            m_hit.gameObject.SetActive(true);
            m_dieBtn.gameObject.SetActive(true);
        }

        public void Idle2()
        {
            m_dragon.SetBool("Walk", false);
            m_dragon.SetBool("Run", false);
            m_dragon.SetBool("Idle2", true);
            m_idle2Btn.gameObject.SetActive(false);
            m_idleBtn.gameObject.SetActive(true);
            m_walkBtn.gameObject.SetActive(true);
            m_runBtn.gameObject.SetActive(true);
        }

        public void Sleep()
        {
            m_dragon.SetBool("Sleep", true);
            SetAllButtonActive(false);
            m_WakeUpBtn.gameObject.SetActive(true);    
        }

        public void WakeUp()
        {
            m_dragon.SetBool("Sleep", false);
            SetAllButtonActive(true);
            m_WakeUpBtn.gameObject.SetActive(false);
            ShowFlyModeButtons(false);
        }

        public void Die()
        {
            m_dragon.SetBool("Die", true);
            SetAllButtonActive(false);
            m_respawn.gameObject.SetActive(true);
            m_ground.m_speed = 10f;

            if (m_ground.m_flyMode)
                StartCoroutine(FallOnGround());
        }

        IEnumerator FallOnGround()
        {
            yield return new WaitForSeconds(3f);
            m_ground.m_flyMode = false;
        }

        public void Respawn()
        {
            m_dragon.SetBool("Fly", false);
            m_dragon.SetBool("Fly_plane", false);
            m_dragon.SetBool("Fly_stationary", false);
            m_dragon.SetBool("Idle2", false);
            m_dragon.SetBool("Sleep", false);
            m_dragon.SetBool("Taunt", false);
            m_dragon.SetBool("Attack1", false);
            m_dragon.SetBool("Attack2", false);
            m_dragon.SetBool("Hit1", false);
            m_dragon.SetBool("Hit2", false);
            m_dragon.SetBool("Walk", false);
            m_dragon.SetBool("Run", false);
            m_dragon.SetBool("Die", false);
            ShowFlyModeButtons(false);
            m_ground.m_speed = 1f;
        }


        public void Stationary()
        {
            m_dragon.SetBool("Fly_stationary", true);
            m_dragon.SetBool("Fly_plane", false);
            m_stationaryBtn.gameObject.SetActive(false);
            m_planeBtn.gameObject.SetActive(true);
            m_flyBtn.gameObject.SetActive(true);
        }

        public void Plane()
        {
            m_dragon.SetBool("Fly_plane", true);
            m_dragon.SetBool("Fly_stationary", false);
            m_stationaryBtn.gameObject.SetActive(true);
            m_planeBtn.gameObject.SetActive(false);
            m_flyBtn.gameObject.SetActive(true);
        }


        public void Taunt()
        {
            m_dragon.SetBool("Taunt", true);
            StartCoroutine(ChangeNextFrame("Taunt", false));
        }


        public void Hit()
        {
            string hit = "Hit " + (Random.value > .5 ? 1 : 2);
            m_dragon.SetBool(hit, true);
            StartCoroutine(ChangeNextFrame(hit, false));
        }


        public void SetAllButtonActive( bool active )
        {
            m_WakeUpBtn.gameObject.SetActive(active);
            m_flyBtn.gameObject.SetActive(active);
            m_walkBtn.gameObject.SetActive(active);
            m_runBtn.gameObject.SetActive(active);
            m_Attack1Btn.gameObject.SetActive(active);
            m_Attack2Btn.gameObject.SetActive(active);
            m_SleepBtn.gameObject.SetActive(active);
            m_idle2Btn.gameObject.SetActive(active);
            m_idleBtn.gameObject.SetActive(active);
            m_landBtn.gameObject.SetActive(active);
            m_stationaryBtn.gameObject.SetActive(active);
            m_planeBtn.gameObject.SetActive(active);
            m_respawn.gameObject.SetActive(active);
            m_dieBtn.gameObject.SetActive(active);
            m_taunt.gameObject.SetActive(active);
            m_hit.gameObject.SetActive(active);
        }


        IEnumerator ChangeNextFrame( string id, bool b )
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            m_dragon.SetBool(id, b);

        }

    }
}