using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    [SerializeField] TriggerTracker Playertrigger;
    
    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public bool isHit;
    public bool isFoundPlayer;

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = (transform.position - player.position).normalized;
        rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        isHit = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isHit = false;
    }

    private void OnDisable()
    {
        isFoundPlayer = false;
    }
}
