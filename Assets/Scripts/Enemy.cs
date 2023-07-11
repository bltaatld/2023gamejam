using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
	public int maxHealth;
	public int health {
		get => _health;
		set{
			_health = value;
			if(health <= 0){
				Die();
			}
			health = Mathf.Min(health, maxHealth);
		}
	}
	private int _health;
	public bool dead {
		get;
		private set;
	}

	protected virtual void Start(){
		health = maxHealth;
	}
	public void Die(){
		dead = true;
		Destroy(gameObject);
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.CompareTag("MainCharacter")){
			Debug.Log("Oncoasdfj");
			collision.collider.GetComponent<MainCharacter>().health--;
		}
	}
}
