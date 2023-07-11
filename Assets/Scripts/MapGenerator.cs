using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour{

	public GeneratableRoom[] generatableRooms;
	public int randomWalkCount;
	public int randomWalkLength;

	public GeneratableRoom[,] map;

	public GameObject testObject;

	void Start(){
		Generate();
	}

	public void Generate(){

		RoomType[,] roomDesignation = new RoomType[5, 5];

		roomDesignation[2, 2] = RoomType.Normal;

		for(int i = 0; i < randomWalkCount; i++){
			Vector2Int walk = new Vector2Int(2, 2);
			for(int j = 0; j < randomWalkLength; j++){
				var directions = new List<Vector2Int>();
				if(walk.x > 0){
					directions.Add(new Vector2Int(-1, 0));
				}
				if(walk.y > 0){
					directions.Add(new Vector2Int(0, -1));
				}
				if(walk.x < roomDesignation.GetLength(0) - 1){
					directions.Add(new Vector2Int(1, 0));
				}
				if(walk.y < roomDesignation.GetLength(1) - 1){
					directions.Add(new Vector2Int(0, 1));
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

		List<Vector2Int> oneEntranceRooms = new List<Vector2Int>();
		List<Vector2Int> twoEntranceRooms = new List<Vector2Int>();
		List<Vector2Int> otherRooms = new List<Vector2Int>();
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

				if(entrance == 1){
					oneEntranceRooms.Add(new Vector2Int(x, y));
				}
				else if(entrance == 2){
					twoEntranceRooms.Add(new Vector2Int(x, y));
				}
				else{
					otherRooms.Add(new Vector2Int(x, y));
				}
			}
		}

		ShuffleList(oneEntranceRooms);
		ShuffleList(twoEntranceRooms);
		ShuffleList(otherRooms);

		List<Vector2Int> outerRooms = new List<Vector2Int>();
		outerRooms.AddRange(oneEntranceRooms);
		outerRooms.AddRange(twoEntranceRooms);
		outerRooms.AddRange(otherRooms);

		map = new GeneratableRoom[roomDesignation.GetLength(0), roomDesignation.GetLength(1)];

		var keyPositions = new Vector2Int[3];

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

		for(int x = 0; x < 5; x++){
			for(int y = 0; y < 5; y++){
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
					//var instantiated = possibleRooms[Random.Range(0, possibleRooms.Count)].Instantiate();
					//instantiated.transform.position = new Vector2(x, y);
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

