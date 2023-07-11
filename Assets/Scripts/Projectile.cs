using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour{
	public float force;
	public float lifetime;
	public bool homing;
	private Transform homingTarget;
	private new Rigidbody2D rigidbody2D;
	private SpriteRenderer spriteRenderer;
	public float homingForce;
	public bool piercing;
	private bool hit = false;
	public int damage;

	void Awake(){
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start(){
		rigidbody2D.AddForce(transform.right * force, ForceMode2D.Impulse);
		StartCoroutine(Decay());
		if(homing){
			homingTarget = null;
			foreach(GameObject i in GameObject.FindGameObjectsWithTag("Enemy")){
				if(homingTarget == null || (i.transform.position - transform.position).sqrMagnitude < (homingTarget.transform.position - transform.position).sqrMagnitude){
					homingTarget = i.transform;
				}
			}
		}
	}

	void FixedUpdate(){
		if(homing && homingTarget != null){
			rigidbody2D.AddForce(((Vector2)homingTarget.position - (Vector2)transform.position).normalized * Time.fixedDeltaTime * homingForce);
		}
	}

	private IEnumerator Decay(){
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(hit){
			return;
		}
		if(other.CompareTag("Enemy")){
			Enemy enemy = other.GetComponent<Enemy>();
			if(enemy.dead){
				return;
			}
			if(!piercing){
				hit = true;
				Destroy(gameObject);
			}
			enemy.health -= damage;
		}
	}
}
