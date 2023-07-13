using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class MainCharacter : MoveCharacter{
	public int coin{
		get => _coin;
		set{
			_coin = value;
			UpdateCoinUI();
		}
	}
	public TextMeshProUGUI coinText;
	private int _coin;
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
			_health = Mathf.Min(health, maxHealth);
			UpdateHealthUI();
		}
	}
	private int _health;
	private Animator animator;
	public SubCharacter subCharacter;
	private SpriteRenderer spriteRenderer;
	
	[Header("Contact")]
	public bool contactDeath;
	public bool contactExplosion;
	public GameObject contactExplosionPrefab;
	[Header("Vampirism")]
	public bool vampirism;
	public float vampirismChance;

	[Header("Immunity")]
	public float damageImmuneDuration;
	public float immuneIntensifyTime;
	public Color immuneBlinkColor;
	[HideInInspector] public bool immune;
	[SerializeField] float immuneBlinkInterval;
	[SerializeField] float intenseImmuneBlinkInterval;
	private IEnumerator immuneCoroutine;

	[Header("Shielding")]
	public bool roomEnterShielding;
	private bool shielded;
	[Header("RoomEnterImmune")]
	public float roomEnterImmuneDuration = 5f;
	public bool roomEnterImmune;

	private ColorPulse colorPulse;

	protected override void Awake(){
		base.Awake();
		animator = GetComponent<Animator>();
		subCharacter = GameObject.FindGameObjectWithTag("SubCharacter").GetComponent<SubCharacter>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		colorPulse = GetComponent<ColorPulse>();

	}

	void Start(){
		if(Character.loadDataOnLoadScene){
			LoadValues();
			Character.loadDataOnLoadScene = false;
		}
		else{
			maxHealth = startingHealth;
			health = maxHealth;
		}
		UpdateCoinUI();
		UpdateHealthUI();
	}

	void FixedUpdate(){
		MoveLoop();
	}

	void Gameover(){
		SceneManager.LoadScene("Gameover");
	}

	private void UpdateHealthUI(){
		GameObject.FindGameObjectWithTag("Hearts").GetComponent<HeartsManager>().SetHearts(health, maxHealth);
	}

	private void UpdateCoinUI(){
		coinText.text = coin.ToString();
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
		if(shielded){
			shielded = false;
			colorPulse.Pulse(Color.yellow, 1f);
		}
		else{
			health -= damage;
		}
		if(contactExplosion){
			Instantiate(contactExplosionPrefab, transform.position, Quaternion.identity);
		}
		TemporaryImmune(damageImmuneDuration);
	}

	public void TemporaryImmune(float duration){
		IEnumerator TemporaryImmuneCoroutine(){
			immune = true;
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
			immune = false;
			spriteRenderer.color = Color.white;
		}
		if(immuneCoroutine != null){
			StopCoroutine(immuneCoroutine);
		}
		immuneCoroutine = TemporaryImmuneCoroutine();
		StartCoroutine(immuneCoroutine);
	}
	
	public void OnRoomEnter(){
		if(roomEnterImmune){
			TemporaryImmune(roomEnterImmuneDuration);
		}
		if(roomEnterShielding){
			shielded = true;
		}
	}

	public void SaveValues(){
		CharacterData data = new CharacterData();
		data.maxHealth = maxHealth;
		data.health = health;
		data.roomEnterImmune = roomEnterImmune;
		data.contactDeath = contactDeath;
		data.contactExplosion = contactExplosion;
		data.vampirism = vampirism;
		data.roomEnterShielding = roomEnterShielding;
		data.coin = coin;

		// subcharacter stuff
		data.attackDelay = subCharacter.attackDelay;
		data.attackRecoil = subCharacter.attackRecoil;
		data.attackDamage = subCharacter.attackDamage;
		data.doubleshot = subCharacter.doubleshot;
		data.tripleshot = subCharacter.tripleshot;
		data.homing = subCharacter.homing;
		data.piercing = subCharacter.piercing;
		data.explosive = subCharacter.explosive;
		data.split = subCharacter.split;
		Character.characterData = data;
	}

	public void LoadValues(){
		CharacterData data = Character.characterData;
		maxHealth = data.maxHealth;
		health = data.health;
		roomEnterImmune = data.roomEnterImmune;
		contactDeath = data.contactDeath;
		contactExplosion = data.contactExplosion;
		vampirism = data.vampirism;
		roomEnterShielding = data.roomEnterShielding;
		coin = data.coin;

		// subcharacter stuff
		subCharacter.attackDelay = data.attackDelay;
		subCharacter.attackRecoil = data.attackRecoil;
		subCharacter.attackDamage = data.attackDamage;
		subCharacter.doubleshot = data.doubleshot;
		subCharacter.tripleshot = data.tripleshot;
		subCharacter.homing = data.homing;
		subCharacter.piercing = data.piercing;
		subCharacter.explosive = data.explosive;
		subCharacter.split = data.split;
	}

	public void OnCollisionEnter2D(Collision2D collision){
		if(contactDeath){
			var enemy = collision.collider.GetComponent<Enemy>();
			if(enemy != null){
				enemy.Die();
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Coin")){
			AudioManager.instance.PlaySound(1);
			Destroy(other.gameObject);
			coin++;
			return;
		}
	}

	public void EnemyDeath(){
		if(vampirism && Random.value < vampirismChance){
			health++;
		}
	}

}
