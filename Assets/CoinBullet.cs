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
            var mainCharacter = collision.gameObject.GetComponent<MainCharacter>();
            if(!mainCharacter.immune){
                mainCharacter.Damage(1);
            }
            Instantiate(Effect);
            Destroy(gameObject);  
        }
    }
}
