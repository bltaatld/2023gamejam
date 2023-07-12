using UnityEngine;

public class ContactDeathItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.contactDeath = true;
	}
}
