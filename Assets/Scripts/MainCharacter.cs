using UnityEngine;

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
			Debug.Log("health set");
			_health = value;
			if(health <= 0){
				Gameover();
			}
			UpdateHealthUI();
		}
	}
	private int _health;
	private Animator animator;
	private SubCharacter subCharacter;
	public bool doubleshot;
	public bool tripleshot;
	public float immuneTime;
	private float immuneTimer;
	public bool immune => immuneTimer > immuneTime;

	protected override void Awake(){
		base.Awake();
		animator = GetComponent<Animator>();
		subCharacter = GameObject.FindGameObjectWithTag("SubCharacter").GetComponent<SubCharacter>();
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
		immuneTimer -= Time.deltaTime;
		immuneTimer = Mathf.Max(0f, immuneTimer);
	}

	public void MoveTo(Vector2 position){
		transform.position = position;
		subCharacter.transform.position = position;
	}

	public void Damage(int damage){
		health -= damage;
		immuneTimer = immuneTime;
	}
}
