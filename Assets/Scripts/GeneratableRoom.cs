using UnityEngine;

[CreateAssetMenu]
public class GeneratableRoom : ScriptableObject{

	public GameObject roomGameObject;
	public RoomType roomType;
	public bool eastDoor;
	public bool westDoor;
	public bool northDoor;
	public bool southDoor;

	public bool Spawnable(bool east, bool west, bool north, bool south){
		return (!east || eastDoor) &&
		(!west || westDoor) &&
		(!north || northDoor) &&
		(!south || southDoor);
	}
}

