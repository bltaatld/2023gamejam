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
	public GameObject endParticle;
	public GameObject explosion;
	public bool explosive;
	public GameObject splitProjectile;
	public bool split;

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
		Instantiate(endParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(hit){
			return;
		}
		if(other.CompareTag("Enemy")){
			if(explosive){
				Instantiate(explosion, transform.position, Quaternion.identity);
			}
			if(split){
				float[] rotationAngles = {45, -45, 135, -135};
				foreach(float i in rotationAngles){
					var instantiated = Instantiate(splitProjectile, transform.position, transform.rotation);
					instantiated.transform.Rotate(0, 0, i);
					instantiated.GetComponent<Projectile>().split = false;
				}
			}
			Instantiate(endParticle, transform.position, Quaternion.identity);
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
