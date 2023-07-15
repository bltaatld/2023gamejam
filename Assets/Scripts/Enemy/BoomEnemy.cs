using System.Collections;
using System.Collections.Generic;
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

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("MainCharacter").gameObject;
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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
        base.Die();
    }

    public void BoomSoundPlay()
    {
        AudioManager.instance.PlaySound(3);
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
