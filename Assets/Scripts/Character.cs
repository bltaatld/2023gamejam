using UnityEngine;

[System.Serializable]
public class CharacterData{
	public int maxHealth;
	public int health;
	public int attackDamage;
	public float shotSpeed;
	public bool piercing;
	public bool homing;
	public float movementSpeed;
	public bool doubleshot;
	public bool tripleshot;
	public bool roomEnterImmune;
}

public static class Character{
	public static CharacterData characterData;
}
