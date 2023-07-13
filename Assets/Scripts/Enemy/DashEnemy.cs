using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public Animator anim;

    [SerializeField] TriggerTracker Playertrigger;

    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public bool isHit;
    public bool isFoundPlayer = false;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("MainCharacter").gameObject;
    }

    void FixedUpdate()
    {
        if (Playertrigger.triggered)
        {
            isFoundPlayer = true;
        }

        if (isFoundPlayer)
        {
            anim.SetTrigger("IsDash");
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        // 플레이어 위치에 따라 스프라이트를 좌우로 뒤집기
        if (player.position.x < transform.position.x)
        {
            // 플레이어가 상대적으로 왼쪽에 있는 경우
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            // 플레이어가 상대적으로 오른쪽에 있는 경우
            GetComponent<SpriteRenderer>().flipX = true;
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
