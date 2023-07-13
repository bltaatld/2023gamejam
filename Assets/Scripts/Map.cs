using UnityEngine;

public class Map : MonoBehaviour{
	public Room focusedRoom{
		get => _focusedRoom;
		set {
			roomMove = true;
			_focusedRoom = value;
			targetPosition = focusedRoom.transform.position;
			mainCharacter.OnRoomEnter();
			CheckRoom();
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
	private MainCharacter mainCharacter;

	public bool roomCleared{
		get => _roomCleared;
		set{
			_roomCleared = value;
			foreach(Door i in focusedRoom.doors){
				if(i.targetDoor != null){
					i.opened = roomCleared;
				}
			}
		}
	}
	private bool _roomCleared;

	private bool roomMove;

	private MapGenerator mapGenerator;

	private void SetCameraPosition(Vector2 position){
		Camera.main.transform.position = new Vector3(position.x, position.y, -10);
	}

	private Room _focusedRoom;

	void Awake(){
		mapGenerator = GetComponent<MapGenerator>();
	}
	
	void Start(){
		mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<MainCharacter>();
		mapGenerator.Generate();
		focusedRoom = mapGenerator.rooms[mapGenerator.startingPosition.x, mapGenerator.startingPosition.y];
		CheckRoom();
	}

	void Update(){
		progress += Time.deltaTime;
		progress = Mathf.Min(progress, cameraMoveDuration);
		SetCameraPosition(Vector2.Lerp(originalPosition, targetPosition, progress / cameraMoveDuration));
		mainCharacter.unmovable = progress != cameraMoveDuration;
		if(roomMove && progress == cameraMoveDuration){
			focusedRoom.enemies.SetActive(true);
			roomMove = false;
			CheckRoom();
		}
	}

	public void EnemyDeath(){
		CheckRoom();
	}

	private void CheckRoom(){
		var cleared = true;
		foreach(GameObject i in GameObject.FindGameObjectsWithTag("Enemy")){
			if(!i.GetComponent<Enemy>().dead){
				cleared = false;
				break;
			}
		}
		roomCleared = cleared;
	}
}
