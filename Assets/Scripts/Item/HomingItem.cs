using UnityEngine;

public class HomingItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.homing = true;
	}
}
