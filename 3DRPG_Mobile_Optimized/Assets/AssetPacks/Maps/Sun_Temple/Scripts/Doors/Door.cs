using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Door : MonoBehaviour
    {
		public bool IsLocked = false;
		public bool DoorClosed = true;
		public float MaxDistance = 3.0f;
		public bool isSpecial;

		private GameObject Player;
		private CursorManager cursor;

	private Animator anim;
		public GameObject particle;

		void Start()
		{
		anim = GetComponent<Animator>();

			Player = GameManager.Instance.player.gameObject;

			cursor = CursorManager.Instance;

			if (cursor != null)
			{
				cursor.SetCursorToDefault();
			}
		}

        private void OnTriggerEnter(Collider other)
        {
			if(other.gameObject.layer == 9)
            {
				if (IsLocked == false)
				{
					cursor.SetCursorToDoor();
				}
				else if (IsLocked == true)
				{
					cursor.SetCursorToLocked();
				}
			}
		}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
			if(cursor.btnPressed && Mathf.Abs(Vector3.Distance(transform.position, Player.transform.position)) <= MaxDistance)
            {
				Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer == 9)
            {
				cursor.SetCursorToDefault();
			}
        }

		public void Activate()
		{
			if (DoorClosed)
            {
				Open();
			} else
            {
				Close();
            }
		}

		void Open()
		{
		StartCoroutine(ProcessDoor(false, "OPEN"));
			anim.SetBool("isOpen", true);
		}

		void Close()
		{
		StartCoroutine(ProcessDoor(true, "CLOSE"));
		anim.SetBool("isOpen", false);
		}
	IEnumerator ProcessDoor(bool state, string openState)
    {
		yield return new WaitForSeconds(0.2f);
		if (isSpecial && gameObject.layer == 7 && openState.Equals("OPEN"))
        {
			particle.SetActive(true);
        }
		else if(isSpecial && gameObject.layer == 7 && openState.Equals("CLOSE"))
        {
			particle.SetActive(false);
		}
		yield return new WaitForSeconds(0.8f);
		cursor.setBtnPressedFromDoorClass(false);
		DoorClosed = state;
    }
 }