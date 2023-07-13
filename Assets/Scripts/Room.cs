using UnityEngine;

public class Room : MonoBehaviour{
	public Door eastDoor;
	public Door westDoor;
	public Door northDoor;
	public Door southDoor;

	public Door[] doors;

	public Sprite unopenedDoorSprite;
	public Sprite openedDoorSprite;

	public GameObject enemies;


	void Awake(){
		doors = new Door[4];
		doors[0] = eastDoor;
		doors[1] = westDoor;
		doors[2] = northDoor;
		doors[3] = southDoor;
		foreach(Door i in doors){
			i.room = this;
			i.unopenedSprite = unopenedDoorSprite;
			i.openedSprite = openedDoorSprite;
		}
	}
}
