using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour{

	public float explosionForce;
	public float explosionCheckTime;
	private Collider2D explosionCollider;
	public int damage;

	void Start(){
		explosionCollider = GetComponent<Collider2D>();
		StartCoroutine(Decay());
		transform.Rotate(0, 0, Random.Range(0, 360));
	}

	void EndExplosion(){
		Destroy(gameObject);
	}


	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("A");
		if(other.CompareTag("Enemy")){
			other.GetComponent<Rigidbody2D>().AddForce((other.transform.position - transform.position).normalized * explosionForce, ForceMode2D.Impulse);
			other.GetComponent<Enemy>().health -= damage;
			Debug.Log("A");
		}
	}

	private IEnumerator Decay(){
		yield return new WaitForSeconds(explosionCheckTime);
		explosionCollider.enabled = false;
	}
}
