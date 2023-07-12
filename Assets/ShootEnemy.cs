using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public Animator anim;

    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public GameObject projectilePrefab;
    public int projectileCount = 5;
    public float projectileSpeed = 10f;
    public float spreadAngle = 30f;
    public float shootTime;
    public float ShootCoolDown;

    public bool isHit;
    public bool isFoundPlayer;
    public bool isBark;

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

        shootTime += Time.deltaTime;

        if (shootTime >= ShootCoolDown)
        {
            anim.SetTrigger("IsShoot");
            FireProjectiles();
            shootTime = 0f;
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

    public void FireProjectiles()
    {
        Vector2 playerDirection = (player.position - transform.position).normalized;
        float startAngle = -spreadAngle / 2f;
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + (angleIncrement * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 direction = rotation * playerDirection;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = direction * projectileSpeed;
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
}
