using UnityEngine;

public class TripleshotItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.tripleshot = true;
	}
}
