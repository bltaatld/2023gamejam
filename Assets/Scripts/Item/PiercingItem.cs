using UnityEngine;

public class PiercingItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.piercing = true;
	}
}
