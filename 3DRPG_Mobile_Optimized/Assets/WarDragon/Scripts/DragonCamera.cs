using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCamera : MonoBehaviour
{
    public GameObject target;
    private Vector3 movePos = new Vector3(53.06f, 8f, -17.35f);
    public bool isMove = false;

    public void Awake()
    {
        SoundManager.Instance.playAudio("DragonFly");
    }

    private void FixedUpdate()
    {
        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

        if(isMove)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, 15f * Time.deltaTime);
    }
    public void MoveCamera()
    {
        this.isMove = true;
    }
}
