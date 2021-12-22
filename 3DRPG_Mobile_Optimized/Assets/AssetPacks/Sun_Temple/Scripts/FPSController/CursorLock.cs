using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class CursorLock : MonoBehaviour {

		private bool isLocked;

		void Start(){
			isLocked = true;
		}
		/*
		void Update(){
			
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (isLocked) {
					isLocked = false;
				} else if (!isLocked) {
					isLocked = true;
				}
			}

			if (isLocked) {
				//Cursor.lockState = CursorLockMode.Locked;
			}

			if (!isLocked) {
				//Cursor.lockState = CursorLockMode.None;
			}
		}
		*/
	}
