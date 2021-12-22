using UnityEngine;
	
	public class CursorManager : Singleton<CursorManager>
{
		public Sprite lockedCursor;
		public Sprite doorCursor;
		public bool btnPressed = false;
		public Quest FirstQuest;

	private UnityEngine.UI.Image img;


		void Awake () {
			img = GetComponent<UnityEngine.UI.Image> ();						
		}


		public void SetCursorToLocked(){
			img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
			img.sprite = lockedCursor;
			
		}

		public void SetCursorToDoor(){
			img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
			img.sprite = doorCursor;
		}

		public void SetCursorToDefault(){
			img.color =  new Color(img.color.r, img.color.g, img.color.b, 0f);
		}

	public void setBtnPressed()
    {
		if(FirstQuest.state != QuestState.Startable)
        {
			this.btnPressed = true;
		} else
        {
			NotificationManager.Instance.Generate_CannotOpenTheDoor();
		}
    }

	public void setBtnPressedFromDoorClass(bool state)
    {
		this.btnPressed = state;
    }
	}