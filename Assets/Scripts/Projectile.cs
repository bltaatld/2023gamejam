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
			rigidbody2D.AddForce((transform.position - homingTarget.position) * Time.fixedDeltaTime);
		}
	}

	private IEnumerator Decay(){
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}
}
