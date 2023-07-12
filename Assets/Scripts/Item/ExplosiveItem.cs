using UnityEngine;

public class ExplosiveItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.explosive = true;
	}
}
