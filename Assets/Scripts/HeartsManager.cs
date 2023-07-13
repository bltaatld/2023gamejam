using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartsManager : MonoBehaviour{

	private List<Image> hearts;
	public GameObject heartPrefab;
	public Sprite fullHeartSprite;
	public Sprite halfHeartSprite;
	public Sprite emptyHeartSprite;
	public int heartGap;
	public int heartBelow;
	

	public void SetHearts(int health, int maxHealth){
		Debug.Log(health + " " + maxHealth);
		if(hearts == null){
			hearts = new List<Image>();
		}
		var heartsNeeded = maxHealth / 2;
		var heartsAdding = heartsNeeded - hearts.Count;
		for(int i = 0; i < heartsAdding; i++){
			var instantiated = Instantiate(heartPrefab, transform);
			hearts.Add(instantiated.GetComponent<Image>());
			instantiated.transform.localPosition = new Vector2(heartGap * hearts.Count, -heartBelow);
		}

		for(int i = 0; i < hearts.Count; i++){
			Sprite sprite = null;
			if((i+1) * 2 <= health){
				sprite = fullHeartSprite;
				Debug.Log("Full");
			}
			else if((i+1) * 2 - 1 == health){
				sprite = halfHeartSprite;
				Debug.Log("Half");
			}
			else if(i < heartsNeeded){
				sprite = emptyHeartSprite;
			}
			hearts[i].sprite = sprite;
		}
	}
}
