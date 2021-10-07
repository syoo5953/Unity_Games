using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Hammer, Sword, Gun};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public int maxAmmo;
    public int curAmmo;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Use()
    {
        if(type == Type.Hammer || type == Type.Sword)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        } else if(type == Type.Gun && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }
    
    IEnumerator Swing()
    {
        audioSource.Play();
        if(type== Type.Sword)
        {
            yield return new WaitForSeconds(0.2f); // 0.1초 대기
            meleeArea.enabled = true;
            trailEffect.enabled = true;


            yield return new WaitForSeconds(0.1f); //0.3초 대기
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.3f); //0.3초 대기
            trailEffect.enabled = false;
        } else if(type == Type.Hammer)
        {
            yield return new WaitForSeconds(0.5f); // 0.1초 대기
            meleeArea.enabled = true;
            trailEffect.enabled = true;


            yield return new WaitForSeconds(0.3f); //0.3초 대기
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.3f); //0.3초 대기
            trailEffect.enabled = false;
        }


        yield return new WaitForSeconds(0.3f); //0.3초 대기
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f); //0.3초 대기
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        audioSource.Play();
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 100;

        yield return null;

        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody bulletCaseRigid = instantBullet.GetComponent<Rigidbody>();    
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletCaseRigid.AddForce(caseVec, ForceMode.Impulse);
        bulletCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

        yield return null;
    }
}
