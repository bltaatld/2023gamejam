using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour{
	public void LoadScene(string sceneName){
		SceneManager.LoadScene(sceneName);
		Time.timeScale = 1f;
	}

	public void Quit(){
		Application.Quit();
	}
}
