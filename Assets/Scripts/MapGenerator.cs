using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour{

	public GameObject[] generatableRooms;
	public GameObject treasureRoom;
	public GameObject portalRoom;
	public int randomWalkCount;
	public int randomWalkLength;


	public Vector2Int mapSize;
	public Vector2 roomGap;
	public Vector2Int startingPosition;

	public Room[,] rooms;


	public void Generate(){
		var layout = RandomWalkLayout(randomWalkCount, randomWalkLength);
		List<Vector2Int> specialRooms = SortSpecialRooms(layout);


		rooms = new Room[mapSize.x, mapSize.y];

		for(int x = 0; x < mapSize.x; x++){
			for(int y = 0; y < mapSize.y; y++){
				var current = new Vector2Int(x, y) - startingPosition;
				var position = new Vector2(current.x * roomGap.x, current.y * roomGap.y);

				if(layout[x, y]){
					Vector2Int currentPosition = new Vector2Int(x, y);

					GameObject roomPrefab;
					if(currentPosition == startingPosition){
						roomPrefab = generatableRooms[0];
					}
					else if(currentPosition == specialRooms[1]){
						roomPrefab = treasureRoom;
					}
					else{
						roomPrefab = generatableRooms[Random.Range(0, generatableRooms.Length)];
					}
					var instantiated = Instantiate(roomPrefab, position, Quaternion.identity, transform);
					rooms[x, y] = instantiated.GetComponent<Room>();
				}
			}
		}
		ConnectRoomDoors();
	}
	

	private List<Vector2Int> SortSpecialRooms(bool[,] layout){
		List<Vector2Int>[] specialRooms = new List<Vector2Int>[5];
		for(int i = 0; i < specialRooms.Length; i++){
			specialRooms[i] = new List<Vector2Int>();
		}
		for(int x = 0; x < layout.GetLength(0); x++){
			for(int y = 0; y < layout.GetLength(1); y++){
				if(!layout[x, y]){
					continue;
				}
				if(x == startingPosition.x && y == startingPosition.y){
					continue;
				}
				int entranceCount = 0;
				if(x > 0 && layout[x - 1, y]){
					entranceCount++;
				}
				if(y > 0 && layout[x, y - 1]){
					entranceCount++;
				}
				if(x < layout.GetLength(0) - 1 && layout[x + 1, y]){
					entranceCount++;
				}
				if(y < layout.GetLength(1) - 1 && layout[x, y + 1]){
					entranceCount++;
				}
				specialRooms[entranceCount].Add(new Vector2Int(x, y));
			}
		}

		List<Vector2Int> sortedSpecialRooms = new List<Vector2Int>();

		for(int i = 0; i < specialRooms.Length; i++){
			sortedSpecialRooms.AddRange(specialRooms[i]);
		}
		return sortedSpecialRooms;
	}

	private void ConnectRoomDoors(){
		for(int x = 0; x < mapSize.x; x++){
			for(int y = 0; y < mapSize.y; y++){
				if(rooms[x, y] != null){
					if(x != mapSize.x - 1 && rooms[x + 1, y] != null){
						Door.Connect(rooms[x, y].eastDoor, rooms[x + 1, y].westDoor);
						rooms[x, y].eastDoor.opened = true;
						rooms[x + 1, y].westDoor.opened = true;
					}
					if(y != mapSize.y - 1 && rooms[x, y + 1] != null){
						Door.Connect(rooms[x, y].northDoor, rooms[x, y + 1].southDoor);
						rooms[x, y].northDoor.opened = true;
						rooms[x, y + 1].southDoor.opened = true;
					}
				}
			}
		}
	}
	/*
	private RoomType[,] GenerateRoomTypes(){

		var layout = RandomWalkLayout(randomWalkCount, randomWalkLength);

		List<Vector2Int>[] roomsByEntrance = new List<Vector2Int>[5];
		for(int i = 0; i < roomsByEntrance.Length; i++){
			roomsByEntrance[i] = new List<Vector2Int>();
		}

		for(int x = 0; x < layout.GetLength(0); x++){
			for(int y = 0; y < layout.GetLength(1); y++){
				int entrance = 0;
				if(x > 0 && layout[x - 1, y]){
					entrance++;
				}
				if(y > 0 && layout[x, y - 1]){
					entrance++;
				}
				if(x < layout.GetLength(0) - 1 && layout[x + 1, y]){
					entrance++;
				}
				if(y < layout.GetLength(1) - 1 && layout[x, y + 1]){
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

		var specialRooms = new List<RoomType>();

		specialRooms.Add(RoomType.Treasure);
		specialRooms.Add(RoomType.Shop);
		if(Random.Range(0, 5) < 3){
			specialRooms.Add(RoomType.Pond);
		}

		RoomType[,]

		for(int i = 0; i < outerRooms.Length; i ++){
			spe
		}

		for(int i = 0; i < roomTypes.Count; i++){
			var current = outerRooms[i];
			roomDesignation[current.x, current.y] = roomTypes[i];
		}
	}
	*/


	private bool[,] RandomWalkLayout(int randomWalkCount, int randomWalkLength){
		bool[,] layout = new bool[mapSize.x, mapSize.y];
		layout[startingPosition.x, startingPosition.y] = true;
		
		for(int i = 0; i < randomWalkCount; i++){
			Vector2Int walk = startingPosition;
			for(int j = 0; j < randomWalkLength; j++){

				var directions = new List<Vector2Int>();
				if(walk.x < layout.GetLength(0) - 1){
					directions.Add(new Vector2Int(1, 0));
				}
				if(walk.x > 0){
					directions.Add(new Vector2Int(-1, 0));
				}
				if(walk.y < layout.GetLength(1) - 1){
					directions.Add(new Vector2Int(0, 1));
				}
				if(walk.y > 0){
					directions.Add(new Vector2Int(0, -1));
				}

				walk += directions[Random.Range(0, directions.Count)];

				if(layout[walk.x, walk.y]){
					j--;
				}
				else{
					layout[walk.x, walk.y] = true;
				}
			}
		}
		return layout;
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
