using UnityEngine;

public class YSort : MonoBehaviour{
	private SpriteRenderer spriteRenderer;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update(){
		spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 1000);
	}
}

