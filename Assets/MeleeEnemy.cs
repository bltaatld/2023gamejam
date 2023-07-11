using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public Animator anim;

    [SerializeField] TriggerTracker Playertrigger;

    public float bounceForce = 5f;
    public float currentmoveSpeed;
    public float attackCooldown;
    public float AttackTime;

    public bool isHit;
    public bool isFoundPlayer;

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        if (Playertrigger.triggered)
        {
            AttackTime += Time.deltaTime;

            if (AttackTime >= attackCooldown)
            {
                anim.SetTrigger("IsAttack");
                AttackTime = 0f;
            }
        }

        // �÷��̾� ��ġ�� ���� ��������Ʈ�� �¿�� ������
        if (player.position.x < transform.position.x)
        {
            // �÷��̾ ��������� ���ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            // �÷��̾ ��������� �����ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = true;
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
