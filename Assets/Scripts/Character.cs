using UnityEngine;

[System.Serializable]
public class CharacterData{
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
}
