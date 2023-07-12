using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class MainCharacter : MoveCharacter{
	public int startingHealth;
	public int maxHealth{
		get => _maxHealth;
		set{
			_maxHealth = value;
			UpdateHealthUI();
		}
	}
	private int _maxHealth;
	public int health{
		get => _health;
		set{
			_health = value;
			if(health <= 0){
				Gameover();
			}
			UpdateHealthUI();
		}
	}
	private int _health;
	private Animator animator;
	public SubCharacter subCharacter;
	private SpriteRenderer spriteRenderer;

	[Header("Immunity")]
	public float damageImmuneDuration;
	public float immuneIntensifyTime;
	public Color immuneBlinkColor;
	[HideInInspector] public bool immune;
	[SerializeField] float immuneBlinkInterval;
	[SerializeField] float intenseImmuneBlinkInterval;

	[Header("RoomEnterImmune")]
	public float roomEnterImmuneDuration = 5f;
	public bool roomEnterImmune;

	protected override void Awake(){
		base.Awake();
		animator = GetComponent<Animator>();
		subCharacter = GameObject.FindGameObjectWithTag("SubCharacter").GetComponent<SubCharacter>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start(){
		maxHealth = startingHealth;
		health = maxHealth;
		UpdateHealthUI();
	}

	void FixedUpdate(){
		MoveLoop();
	}

	void Gameover(){
		Debug.Log("Gameover");
	}

	private void UpdateHealthUI(){
		GameObject.FindGameObjectWithTag("Hearts").GetComponent<HeartsManager>().SetHearts(health, maxHealth);
	}

	void Update(){
		if(movement == Vector2.zero){
			animator.SetBool("Moving", false);
		}
		else{
			animator.SetBool("Moving", true);
			if(Mathf.Abs(movement.x) >= Mathf.Abs(movement.y)){
				if(movement.x > 0){
					animator.SetInteger("MoveDirection", 0);
				}
				else{
					animator.SetInteger("MoveDirection", 1);
				}
			}
			else{
				if(movement.y > 0){
					animator.SetInteger("MoveDirection", 2);
				}
				else{
					animator.SetInteger("MoveDirection", 3);
				}
			}
		}
	}

	public void MoveTo(Vector2 position){
		transform.position = position;
		subCharacter.transform.position = position;
	}

	public void Damage(int damage){
		health -= damage;
		TemporaryImmune(damageImmuneDuration);
	}

	public void TemporaryImmune(float duration){
		Debug.Log(damageImmuneDuration);
		Debug.Log(duration);
		IEnumerator TemporaryImmuneCoroutine(){
			immune = true;
			Debug.Log("A");
			bool inBlinkColor = false;
			while(duration > 0){
				inBlinkColor = !inBlinkColor;
				if(inBlinkColor){
					spriteRenderer.color = immuneBlinkColor;
				}
				else{
					spriteRenderer.color = Color.white;
				}

				float blink = Mathf.Min(duration, duration <= immuneIntensifyTime ? intenseImmuneBlinkInterval : immuneBlinkInterval);
				yield return new WaitForSeconds(blink);
				duration -= blink;
			}
			Debug.Log("s");
			immune = false;
			spriteRenderer.color = Color.white;
		}
		StartCoroutine(TemporaryImmuneCoroutine());
	}
	
	public void OnRoomEnter(){
		if(roomEnterImmune){
			TemporaryImmune(roomEnterImmuneDuration);
		}
	}

	public void SaveValues(){
		CharacterData data = new CharacterData();
		data.health = health;
		data.maxHealth = health;
		data.roomEnterImmune = roomEnterImmune;

		// subcharacter stuff
		data.doubleshot = subCharacter.doubleshot;
		data.tripleshot = subCharacter.doubleshot;
		data.attackDamage = subCharacter.attackDamage;
		Character.characterData = data;
	}

	public void loadValues(){
		CharacterData data = Character.characterData;
		health = data.health;
		health = data.maxHealth;
		roomEnterImmune = data.roomEnterImmune;

		// subcharacter stuff
		subCharacter.doubleshot = data.doubleshot;
		subCharacter.doubleshot = data.tripleshot;
		subCharacter.attackDamage = data.attackDamage;
	}
}
