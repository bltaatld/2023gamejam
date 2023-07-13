using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss1 : Enemy
{
    public GameObject PlayerMove;
    public GameObject prefabToInstantiate;
    public TriggerTracker hitboxTrigger;
    public TriggerTracker hitboxTrigger1;
    public Animator animator;
    public Slider bossBar;

    public int randomAction;
    public float actionTimer = 0f;   // 동작 타이머
    public float actionInterval = 3f; // 동작 간격 시간
    public int damage;

    public bool isFoundPlayer;

    protected override void Start()
    {
        base.Start();
        PlayerMove = GameObject.FindGameObjectWithTag("MainCharacter").gameObject;
    }

    private void OnDisable()
    {
        bossBar.gameObject.SetActive(false);
    }

    public override void Die()
    {
        SceneManager.LoadScene("Ending");
    }

    private void Update()
    {
        actionTimer += Time.deltaTime;
        bossBar.maxValue = base.maxHealth;
        bossBar.value = base.health;

        if (actionTimer >= actionInterval)
        {
            randomAction = Random.Range(0, 3);
            this.StartCoroutine(PerformAction(randomAction));

            actionTimer = 0f;
        }

        if (hitboxTrigger.triggered)
        {
            if (!isFoundPlayer)
            {
                PlayerMove.GetComponent<MainCharacter>().Damage(damage);
                isFoundPlayer = true;
            }
        }

        if (hitboxTrigger1.triggered)
        {
            if (!isFoundPlayer)
            {
                PlayerMove.GetComponent<MainCharacter>().Damage(damage);
                isFoundPlayer = true;
            }
        }
    }

    public void SetDamageValue(int Value)
    {
        damage = Value;
    }

    public void CheckReset()
    {
        isFoundPlayer = false;
    }

    public void InstantiatePrefabInRandomPosition()
    {
        // 프리팹 생성
        Instantiate(prefabToInstantiate, PlayerMove.transform.position, Quaternion.identity);
    }

    public void SmashSoundPlay()
    {
        AudioManager.instance.PlaySound(6);
    }

    IEnumerator PerformAction(int action)
    {
        switch (action)
        {
            case 0:
                // 정지
                yield return new WaitForSeconds(1f);
                break;
            case 1:
                animator.SetTrigger("IsAttack1");
                yield return new WaitForSeconds(1f);
                break;
            case 2:
                animator.SetTrigger("IsAttack2");
                yield return new WaitForSeconds(1f);
                break;
        }
    }
}
