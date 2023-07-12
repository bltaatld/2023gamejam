using UnityEngine;

public class DamageItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.attackDamage++;
	}
}
