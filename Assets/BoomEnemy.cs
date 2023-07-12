using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class BoomEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public int BoomCount;
    public int MaxBoomCount;

    public bool isHit;
    public bool isFoundPlayer;

    public Animator anim;

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
        if (collision.gameObject.CompareTag("MainCharacter"))
        {
            Vector2 direction = (transform.position - player.position).normalized;
            rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            isHit = true;
        }
    }
    private void Update()
    {
        Invoke("Boom", 0.2f * Time.deltaTime);
    }

    void Boom()
    {
        BoomCount++;

        if (BoomCount >= MaxBoomCount)
        {
            anim.SetTrigger("Boom");
        }
    }

    public void DisactiveObject()
    {
        gameObject.SetActive(false);
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MainCharacter"))
        {
            isHit = false;
        }
    }

    private void OnDisable()
    {
        isFoundPlayer = false;
    }
}