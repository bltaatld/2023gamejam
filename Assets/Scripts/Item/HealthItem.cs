using UnityEngine;

public class HealthItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.health = mainCharacter.maxHealth;
	}
}
