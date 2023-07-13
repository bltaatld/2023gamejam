using UnityEngine;
using TMPro;

public class Sale : MonoBehaviour{
	public int minPrice;
	public int maxPrice;
	[HideInInspector] public int price;
	public TextMeshPro priceText;
	public GameObject randomItem;
	private GameObject item;

	void Start(){
		price = Random.Range(minPrice, maxPrice);
		priceText.text = price.ToString();
		item = randomItem.transform.GetChild(0).gameObject;
		item.GetComponent<Collider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("MainCharacter")){
			var mainCharacter = other.GetComponent<MainCharacter>();
			if(mainCharacter.coin >= price){
				mainCharacter.coin -= price;
				Obtain();
			}
		}
	}

	private void Obtain(){
		priceText.gameObject.SetActive(false);
		item.GetComponent<Collider2D>().enabled = true;
	}
}
