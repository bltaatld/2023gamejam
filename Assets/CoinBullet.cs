using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBullet : MonoBehaviour
{
    public GameObject Effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCharacter"))
        {
            collision.gameObject.GetComponent<MainCharacter>().Damage(1);
            Instantiate(Effect);
            Destroy(gameObject);  
        }
    }
}
