using UnityEngine;

[System.Serializable]
public class CharacterData{
	public int maxHealth;
	public int health;
	public int damage;
	public float shotSpeed;
	public bool piercing;
	public bool homing;
	public float movementSpeed;
}

public static class Character{
	public static CharacterData characterData;
}
