using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour{
	public GameObject[] items;

	void Start(){
		Instantiate(items[Random.Range(0, items.Length)], transform.position, transform.rotation);
	}
}
