using UnityEngine;

public class ContactExplosionItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.contactExplosion = true;
	}
}
