using UnityEngine;

public class SubCharacter : MoveCharacter{
	[Header("Connection")]
	public float maxDistance;
	public Transform connectTo;
	public float connectionPull;
	public float connectionPush;
	[Header("Attack")]
	public KeyCode attackKeyCode;
	public float attackSemiautoDelay;
	public float attackAutoDelay;
	public float attackRecoil;
	public float attackDamage;
	public GameObject attackProjectile;
	private float lastAttack;

	protected override void Awake(){
		base.Awake();
		lastAttack = attackAutoDelay;
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
		lastAttack = Mathf.Min(lastAttack, attackAutoDelay);
		if(Input.GetKeyDown(attackKeyCode) && lastAttack >= attackSemiautoDelay || Input.GetKey(attackKeyCode) && lastAttack >= attackAutoDelay){
			lastAttack = 0;
			Shoot();
		}
	}

	private void Shoot(){
		var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var instantiated = Instantiate(attackProjectile);
		instantiated.transform.position = transform.position;

		instantiated.transform.right = mousePosition - (Vector2)instantiated.transform.position;

		var recoil= ((Vector2)transform.position - mousePosition).normalized * attackRecoil;

		AddForce(recoil);
	}
}

