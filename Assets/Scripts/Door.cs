using UnityEngine;

public class Door : MonoBehaviour{
	public Door targetDoor;
	public Room room;
	public bool opened{
		get => _opened;
		set{
			_opened = value;
			if(opened){
				spriteRenderer.sprite = openedSprite;
			}
			else{
				spriteRenderer.sprite = unopenedSprite;
			}
			closeCollider.enabled = !opened;
		}
	}
	public Sprite openedSprite;
	public Sprite unopenedSprite;
	private bool _opened;
	private SpriteRenderer spriteRenderer;
	public Collider2D closeCollider;

	public bool connected => targetDoor != null;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		opened = false;
	}

	public static void Connect(Door a, Door b){
		a.gameObject.SetActive(true);
		b.gameObject.SetActive(true);
		a.targetDoor = b;
		b.targetDoor = a;
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("MainCharacter")){
			other.GetComponent<MainCharacter>().MoveTo(targetDoor.transform.position);
			GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().focusedRoom = targetDoor.room;
		}
	}

}

