using UnityEngine;

public class RoomEnterShieldingItem : Item{

	protected override void Obtain(MainCharacter mainCharacter){
		mainCharacter.roomEnterShielding = true;
	}
}
