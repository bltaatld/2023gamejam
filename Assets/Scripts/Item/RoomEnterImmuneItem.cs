using UnityEngine;

public class RoomEnterImmuneItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.roomEnterImmune = true;
	}
}
