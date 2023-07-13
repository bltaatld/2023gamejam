using UnityEngine;

public class VampirismItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.vampirism = true;
	}
}
