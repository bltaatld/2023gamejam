using UnityEngine;

public class MovementSpeedItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.moveForce += 50;
	}
}
