using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public GameObject prefabToInstantiate;
    public Rigidbody2D rigid;

    public float bounceForce = 5f;
    public float instantiateDelay = 3f;
    private float instantiateTimer = 0f;
    public float spawnRadius = 5f;

    public bool isHit;
    public bool isFoundPlayer;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("MainCharacter").gameObject;
    }

    void FixedUpdate()
    {
        instantiateTimer += Time.deltaTime;

        if (instantiateTimer >= instantiateDelay)
        {
            InstantiatePrefabInRandomPosition();
            instantiateTimer = 0f;
        }
    }

    private void InstantiatePrefabInRandomPosition()
    {
        // ÇÁ¸®ÆÕ »ý¼º
        Instantiate(prefabToInstantiate, PlayerMove.transform.position, Quaternion.identity);
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
