using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    public GameObject prefabToInstantiate;

    public float bounceForce = 5f;
    public float currentmoveSpeed;
    public float instantiateDelay = 3f;
    private float instantiateTimer = 0f;
    public float spawnRadius = 5f;

    public bool isHit;
    public bool isFoundPlayer;
    public bool isSlow;


    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        instantiateTimer += Time.deltaTime;

        if (instantiateTimer >= instantiateDelay)
        {
            InstantiatePrefabInRandomPosition();
            instantiateTimer = 0f;
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

    private void InstantiatePrefabInRandomPosition()
    {
        // 원형 반경 내의 랜덤한 위치 계산
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0f) + transform.position;

        // 프리팹 생성
        Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
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