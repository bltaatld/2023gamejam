using UnityEngine;

public class SplitItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.subCharacter.split = true;
	}
}
