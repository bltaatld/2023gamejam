using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    public bool isHit;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCharacter"))
        {
            if (isHit == false)
            {
                anim.SetTrigger("Blow");
                collision.gameObject.GetComponent<MainCharacter>().Damage(1);
                isHit = true;
            }
        }
    }

    public void ResetDeal()
    {
        isHit = false;
        Destroy(gameObject);
    }
}
