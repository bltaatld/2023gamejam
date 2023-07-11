using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainCharacter : MoveCharacter{
	public int maxHealth = 6;
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
	private SubCharacter subCharacter;

	protected override void Awake(){
		base.Awake();
		health = maxHealth;
		animator = GetComponent<Animator>();
		subCharacter = GameObject.FindGameObjectWithTag("SubCharacter").GetComponent<SubCharacter>();
	}

	void FixedUpdate(){
		MoveLoop();
	}

	void Gameover(){

	}

	private void UpdateHealthUI(){

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
}

