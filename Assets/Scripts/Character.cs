using UnityEngine;

[System.Serializable]
public class CharacterData{
	public int coin;
	public int health;
	public int maxHealth;
	public bool roomEnterImmune;
	public bool contactDeath;
	public bool contactExplosion;
	public bool vampirism;
	public bool roomEnterShielding;
	public float attackDelay;
	public float attackRecoil;
	public int attackDamage;
	public bool doubleshot;
	public bool tripleshot;
	public bool homing;
	public bool piercing;
	public bool explosive;
	public bool split;
}

public static class Character{
	public static bool loadDataOnLoadScene;
	public static CharacterData characterData;

	public static GameObject coinPrefab{
		get{
			if(_coinPrefab == null){
				_coinPrefab = Resources.Load<GameObject>("Coin");
			}
			return _coinPrefab;
		}
	}
	private static GameObject _coinPrefab;
}
