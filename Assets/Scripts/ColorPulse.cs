using UnityEngine;

public class ColorPulse : MonoBehaviour{
	private Color color;
	private float progress;
	private float duration;
	private SpriteRenderer spriteRenderer;
	private bool pulsing = false;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		color = Color.white;
	}
	public void Pulse(Color color, float duration){
		Debug.Log("dfefefefefefefef");
		progress = 0;
		this.color = color;
		this.duration = duration;
		pulsing = true;
	}
	
	void Update(){
		if(pulsing){
			spriteRenderer.color = Color.Lerp(color, Color.white, progress / duration);
			progress = Mathf.Min(progress + Time.deltaTime, duration);
		}
	}
}
