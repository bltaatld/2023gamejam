using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyEnemy : Enemy
{
    public MoveCharacter playerValue;
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    [SerializeField] TriggerTracker Playertrigger;

    public float bounceForce = 5f;
    public float currentmoveSpeed;
    public float playerMoveSpeed;
    public float slowValue;

    public bool isHit;
    public bool isFoundPlayer;
    public bool isSlow;

    private void Start()
    {
        playerMoveSpeed = playerValue.moveForce;
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        if (Playertrigger.triggered)
        {
            if (!isSlow)
            {
                playerValue.moveForce -= slowValue;
                isSlow = true;
            }
        }

        if (!Playertrigger.triggered)
        {
            if (isSlow)
            {
                playerValue.moveForce = playerMoveSpeed;
                isSlow = false;
            }
        }

        if (playerValue.moveForce <= 0)
        {
            playerValue.moveForce = 0f;
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