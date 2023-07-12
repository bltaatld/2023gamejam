using UnityEngine;

public class DoubleshotItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.doubleshot = true;
	}
}
