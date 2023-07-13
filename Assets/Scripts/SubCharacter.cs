using UnityEngine;

public class SubCharacter : MoveCharacter{
	[Header("Connection")]
	public float maxDistance;
	public Transform connectTo;
	public float connectionPull;
	public float connectionPush;
	[Header("Attack")]
	public KeyCode attackKeyCode;
	public float attackDelay;
	public float attackRecoil;
	public int attackDamage;
	public GameObject attackProjectile;
	private float lastAttack;
	public bool doubleshot;
	public bool tripleshot;
	public bool homing;
	public bool piercing;
	public bool explosive;
	public bool split;

	protected override void Awake(){
		base.Awake();
		lastAttack = attackDelay;
	}

	void Update(){
		AttackLoop();
	}

	void FixedUpdate(){
		MoveLoop();
		if(connectTo != null){
			if((connectTo.position - transform.position).magnitude > maxDistance){
				var relative = transform.position - connectTo.position;
				var exceed = relative - relative.normalized * maxDistance;
				AddForce(-exceed * connectionPull * Time.fixedDeltaTime);

				var other = connectTo.GetComponent<MoveCharacter>();
				if(other != null){
					other.AddForce(exceed * connectionPush * Time.fixedDeltaTime);
				}
			}
		}
	}

	private void AttackLoop(){
		lastAttack += Time.deltaTime;
		lastAttack = Mathf.Min(lastAttack, attackDelay);
		if(Input.GetKey(attackKeyCode) && lastAttack >= attackDelay){
			lastAttack = 0;
			Shoot();
		}
	}

	private void Shoot(){
		DoubleShootDirection(0f);
		if(tripleshot){
			DoubleShootDirection(-15f);
			DoubleShootDirection(15f);
		}

		var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var recoil= ((Vector2)transform.position - mousePosition).normalized * attackRecoil;

		AddForce(recoil);
	}

	private void DoubleShootDirection(float angle){
		if(doubleshot){
			ShootDirection(angle, 0.1f);
			ShootDirection(angle, -0.1f);
		}
		else{
			ShootDirection(angle, 0f);
		}
	}
	private void ShootDirection(float angle, float offset){
		var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var instantiated = Instantiate(attackProjectile);
		instantiated.transform.position = transform.position;
		instantiated.transform.right = mousePosition - (Vector2)instantiated.transform.position;
		instantiated.transform.Rotate(0, 0, angle);
		instantiated.transform.Translate(0, offset, 0);
		var projectile = instantiated.GetComponent<Projectile>();
		projectile.damage = attackDamage;
		projectile.homing = homing;
		projectile.piercing = piercing;
		projectile.explosive = explosive;
		projectile.split = split;
	}
}

