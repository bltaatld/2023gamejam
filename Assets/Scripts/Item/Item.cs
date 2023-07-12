using UnityEngine;

public abstract class Item : MonoBehaviour{
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("MainCharacter")){
			Obtain(other.GetComponent<MainCharacter>());
			gameObject.SetActive(false);
		}
	}

	protected abstract void Obtain(MainCharacter mainCharacter);
}

