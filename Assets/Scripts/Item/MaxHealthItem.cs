using UnityEngine;

public class MaxHealthItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.maxHealth += 2;
		mainCharacter.health += 2;
	}
}
