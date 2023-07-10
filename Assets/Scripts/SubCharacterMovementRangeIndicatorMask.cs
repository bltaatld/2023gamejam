using UnityEngine;

public class SubCharacterMovementRangeIndicatorMask : MonoBehaviour{
	[SerializeField] float ratio;
	[SerializeField] Transform mainCharacter;
	[SerializeField] Transform subCharacter;

	void Update(){
		transform.position = Vector3.Lerp(mainCharacter.position, subCharacter.position, ratio);
	}
}

