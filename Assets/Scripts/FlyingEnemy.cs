using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    [SerializeField] TriggerTracker Playertrigger;
    
    public float moveSpeed = 5f;
    public float bounceForce = 5f;
    public float enemyAP = 5f;
    public float currentmoveSpeed;

    public float followTimer;

    public bool isHit;
    public bool isFoundPlayer;

    void Update()
    {
        followTimer += Time.deltaTime;

        currentmoveSpeed = moveSpeed;

        if (Playertrigger.triggered)
        {
            Vector2 direction = (transform.position - player.position).normalized;
            rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            isHit = true;
        }

        if (!Playertrigger.triggered)
        {
            isHit = false;
        }


        if (!isHit)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, currentmoveSpeed * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        isFoundPlayer = false;
    }
}
