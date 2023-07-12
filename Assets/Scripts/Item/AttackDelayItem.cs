using UnityEngine;

public class AttackDelayItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.attackDelay *= 0.7f;
	}
}
