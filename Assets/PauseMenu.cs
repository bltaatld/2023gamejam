using UnityEngine;

public class PauseMenu : MonoBehaviour{
	public KeyCode pauseKey;

	public bool paused{
		get => _paused;
		set{
			_paused = value;
			if(paused){
				Time.timeScale = 0f;
			}
			else{
				Time.timeScale = 1f;
			}
			pauseEnableMenu.SetActive(paused);
		}
	}
	public GameObject pauseEnableMenu;
	public bool _paused;

	void Start(){
		pauseEnableMenu.SetActive(false);
	}

	void Update(){
		if(Input.GetKeyDown(pauseKey)){
			paused = !paused;
		}
	}
}
