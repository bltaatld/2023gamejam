using UnityEngine;

public class Map : MonoBehaviour{
	public Room focusedRoom{
		get => _focusedRoom;
		set {
			_focusedRoom = value;
			Camera.main.transform.position = focusedRoom.transform.position;
			Camera.main.transform.Translate(0, 0, -10);
		
		}
	}

	private Room _focusedRoom;
}
