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
	
	void Awake(){
		hearts = new List<Image>();
	}

	public void SetHearts(int health, int maxHealth){
		var heartsNeeded = (maxHealth + 1) / 2;
		var heartsAdding = heartsNeeded - hearts.Count;
		for(int i = 0; i < heartsAdding; i++){
			var instantiated = Instantiate(heartPrefab, transform);
			hearts.Add(instantiated.GetComponent<Image>());
			instantiated.transform.localPosition = new Vector2(heartGap * hearts.Count, -heartBelow);
		}
		for(int i = 0; i < hearts.Count; i++){
			Sprite sprite = null;
			if(i * 2 + 1 < health){
				sprite = fullHeartSprite;
			}
			else if(i * 2 + 1 == health){
				sprite = halfHeartSprite;
			}
			else if(i < heartsNeeded){
				sprite = emptyHeartSprite;
			}
			hearts[i].sprite = sprite;
		}
	}
}
