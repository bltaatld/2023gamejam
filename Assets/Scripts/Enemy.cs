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
			_health = Mathf.Min(health, maxHealth);
		}
	}
	private int _health;
	public bool dead {
		get;
		private set;
	}

	public GameObject deathParticle;

	protected virtual void Start(){
		health = maxHealth;
	}
	public virtual void Die(){
		if(deathParticle != null){
			Instantiate(deathParticle, transform.position, Quaternion.identity);
		}
		dead = true;
		Destroy(gameObject);
		GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().EnemyDeath();
		GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<MainCharacter>().EnemyDeath();
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.CompareTag("MainCharacter")){
			var mainCharacter = collision.collider.GetComponent<MainCharacter>();
			if(!mainCharacter.immune){
				mainCharacter.Damage(1);
			}
		}
	}
}
