using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour{
	public float force;
	private new Rigidbody2D rigidbody2D;

	void Awake(){
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Start(){
		rigidbody2D.AddForce(transform.right * force, ForceMode2D.Impulse);
	}
}

