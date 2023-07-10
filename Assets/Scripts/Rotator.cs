using UnityEngine;

public class Rotator : MonoBehaviour{
	public float rotationSpeed;

	void Update(){
		transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
	}
}

