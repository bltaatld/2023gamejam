/*using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour{

	public GeneratableRoom[] generatableRooms;
	public int randomWalkCount;
	public int randomWalkLength;

	public GameObject testObject;

	public Vector2Int mapSize;
	public Vector2Int startingPosition;

	void Start(){
		if(randomWalkCount * randomWalkLength > size.x * size.y){
			Debug.LogError("no");
		}
		else{
			Generate();
		}
	}

	public void Generate(){
		var roomDesignation = GenerateRoomTypes();
		var map = new GeneratableRoom[roomDesignation.GetLength(0), roomDesignation.GetLength(1)];

		Room[,] rooms = new Room[x, y];

		for(int x = 0; x < roomDesignation.GetLength(0); x++){
			for(int y = 0; y < roomDesignation.GetLength(1); y++){
				if(roomDesignation[x, y] != RoomType.None){

					List<GeneratableRoom> possibleRooms = new List<GeneratableRoom>();

					for(int i = 0; i < generatableRooms.Length; i++){
						if(generatableRooms[i].roomType == roomDesignation[x, y] && 
								generatableRooms[i].Spawnable(
									x > 0 && roomDesignation[x - 1, y] != RoomType.None, 
									y > 0 && roomDesignation[x, y - 1] != RoomType.None,
									x < roomDesignation.GetLength(0) - 1 && roomDesignation[x + 1, y] != RoomType.None,
									y < roomDesignation.GetLength(1) - 1 && roomDesignation[x, y + 1] != RoomType.None
									)){
							possibleRooms.Add(generatableRooms[i]);
						}
					}
					rooms[x, y] = possibleRooms[Ramdon.Range(0, possibleRooms.Count)].Instantiate();
					rooms[x, y].transform.position = new Vector2(x, y) * 10;
				}
			}
		}


		for(int x = 0; x < roomDesignation.GetLength(0) - 1; x++){
			for(int y = 0; y < roomDesignation.GetLength(1) - 1; y++){
				var current = roomDesignation[x, y];
				if(roomDesignation[x, y] != RoomType.None){
					var east = roomDesignation[x + 1, y];
					var north = roomDesignation[x, y + 1];
					if(east != RoomType.None){
						current.eastDoor.gameObject.SetActive(true);
						east.westDoor.gameObject.SetActive(true);
						Door.connect(current.eastDoor, east.westDoor);
					}
					if(north != RoomType.None){
						Door.Connect(current.northDoor, north.southDoor);
					}
				}
			}
		}
	}

	private RoomType[,] GenerateRoomTypes(){
		var roomDesignation = StartingRoomType();


		List<Vector2Int>[] roomsByEntrance = new List<Vector2Int>[5];
		for(int i = 0; i < roomsByEntrance.Length; i++){
			roomsByEntrance[i] = new List<Vector2Int>();
		}
		for(int x = 0; x < roomDesignation.GetLength(0); x++){
			for(int y = 0; y < roomDesignation.GetLength(1); y++){
				int entrance = 0;
				if(x > 0 && roomDesignation[x - 1, y] != RoomType.None){
					entrance++;
				}
				if(y > 0 && roomDesignation[x, y - 1] != RoomType.None){
					entrance++;
				}
				if(x < roomDesignation.GetLength(0) - 1 && roomDesignation[x + 1, y] != RoomType.None){
					entrance++;
				}
				if(y < roomDesignation.GetLength(1) - 1 && roomDesignation[x, y + 1] != RoomType.None){
					entrance++;
				}

				roomsByEntrance[entrance].Add(new Vector2Int(x, y));
			}
		}
		List<Vector2Int> outerRooms = new List<Vector2Int>();
		for(int i = 0; i < roomsByEntrance.Length; i++){
			ShuffleList(roomsByEntrance[i]);
			outerRooms.AddRange(roomsByEntrance[i]);
		}
		
		var keyPositions = new List<Vector2Int>;

		var roomTypes = new List<RoomType>();

		roomTypes.Add(RoomType.Treasure);
		roomTypes.Add(RoomType.Shop);
		if(Random.Range(0, 5) < 3){
			roomTypes.Add(RoomType.Pond);
		}

		for(int i = 0; i < roomTypes.Count; i++){
			var current = outerRooms[i];
			roomDesignation[current.x, current.y] = roomTypes[i];
		}
	}

	private RoomType[,] StartingRoomType(){
		RoomType[,] roomDesignation = new RoomType[5, 5];
		

		for(int x = 0; x < roomDesignation.GetLength(0); x++){
			for(int y = 0; y < roomDesignation.GetLength(1); y++){
				roomDesignation[x, y] = RoomType.Nome;
			}
		}
		roomDesignation[2, 2] = RoomType.Normal;

		return roomDesignation;
	}

	private bool[,] RandomWalk(int randomWalkCount, int randomWalkLength){
		bool[,] roomLayout = new bool[size.x, size.y];
		roomLayout[startingPosition.x, startingPosition.y] = true;
		
		for(int i = 0; i < randomWalkCount; i++){
			Vector2Int walk = startingPosition;
			for(int j = 0; j < randomWalkLength; j++){

				var directions = new List<Vector2Int>();
				if(walk.x < roomDesignation.GetLength(0) - 1){
					directions.Add(new Vector2Int(1, 0));
				}
				if(walk.x > 0){
					directions.Add(new Vector2Int(-1, 0));
				}
				if(walk.y < roomDesignation.GetLength(1) - 1){
					directions.Add(new Vector2Int(0, 1));
				}
				if(walk.y > 0){
					directions.Add(new Vector2Int(0, -1));
				}

				walk += directions[Random.Range(0, directions.Count)];

				if(roomDesignation[walk.x, walk.y] != RoomType.None){
					j--;
				}
				else{
					roomDesignation[walk.x, walk.y] = RoomType.Normal;
				}
			}
		}
	}

	private void ShuffleList<T>(List<T> list){
		for(int i = 0; i < list.Count; i++){
			int swapIndex = Random.Range(0, list.Count);
			T swapStorage = list[i];
			list[i] = list[swapIndex];
			list[swapIndex] = swapStorage;
		}
	}
}

*/
