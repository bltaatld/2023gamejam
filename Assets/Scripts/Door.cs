using UnityEngine;

public class Door : MonoBehaviour{
	pubilc Door targetDoor;
	public Room room;
	public static void Connect(Door a, Door b){
		a.gameObject.SetActive(true);
		b.gameObject.SetActive(true);
		a.targetDoor = b;
		b.targetDoor = a;
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("MainCharacter")){
		}
	}
}

