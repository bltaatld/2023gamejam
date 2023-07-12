using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour{
	public string sceneName;
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("MainCharacter")){
			other.GetComponent<MainCharacter>().SaveValues();
			Character.loadDataOnLoadScene = true;
			SceneManager.LoadScene(sceneName);
		}
	}
}
