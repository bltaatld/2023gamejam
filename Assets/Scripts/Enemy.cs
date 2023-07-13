using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorPulse))]
public class Enemy : MonoBehaviour{
	public int maxHealth;
	public int health {
		get => _health;
		set{
			if(value < health){
				colorPulse.Pulse(new Color(100f/255f, 190f/255f, 255f/255f), 0.5f);
			}
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
	private ColorPulse colorPulse;

	public int minCoins;
	public int maxCoins;

	public GameObject deathParticle;

	protected virtual void Awake(){
		colorPulse = GetComponent<ColorPulse>();
	}

	protected virtual void Start(){
		health = maxHealth;
	}
	public virtual void Die(){
		if(deathParticle != null){
			Instantiate(deathParticle, transform.position, Quaternion.identity);
		}
		dead = true;
		Destroy(gameObject);
		if(GameObject.FindGameObjectWithTag("Map").GetComponent<Map>() != null){
			GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().EnemyDeath();
		}
		GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<MainCharacter>().EnemyDeath();

		int coinCount = Random.Range(minCoins, maxCoins + 1);

		for(int i = 0; i < coinCount; i++){
			Instantiate(Character.coinPrefab, (Vector2)transform.position + Random.insideUnitCircle * 0.2f, Quaternion.identity);
		}
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
