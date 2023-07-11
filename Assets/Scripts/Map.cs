using UnityEngine;

public class Map : MonoBehaviour{
	public Room focusedRoom{
		get => _focusedRoom;
		set {
			_focusedRoom = value;
			targetPosition = focusedRoom.transform.position;
		}
	}
	private Vector2 originalPosition;
	private Vector2 targetPosition{
		get => _targetPosition;
		set{
			originalPosition = Camera.main.transform.position;
			_targetPosition = value;
			progress = 0;
		}
	}
	private Vector2 _targetPosition;
	public float progress;
	public float cameraMoveDuration;

	private void SetCameraPosition(Vector2 position){
		Camera.main.transform.position = new Vector3(position.x, position.y, -10);
	}

	private Room _focusedRoom;

	void Update(){
		progress += Time.deltaTime;
		progress = Mathf.Min(progress, cameraMoveDuration);
		SetCameraPosition(Vector2.Lerp(originalPosition, targetPosition, progress / cameraMoveDuration));
	}
}
