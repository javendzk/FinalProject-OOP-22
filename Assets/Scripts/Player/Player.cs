using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator; 

    private HealthComponent healthComponent;
    private OxygenComponent oxygenComponent;

    [SerializeField] private int fishCount;
    [SerializeField] private int moneyCount;
    [SerializeField] private float oxygenMultiplier;
    [SerializeField] private float moveSpeedMultiplier;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseTank;
    [SerializeField] private float baseBulletDamage;
    [SerializeField] public bool boughtGun;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("PlayerVisual").GetComponent<Animator>();
        healthComponent = GetComponent<HealthComponent>();
        oxygenComponent = GetComponent<OxygenComponent>();
    }

    void Update()
    {
        oxygenComponent.UpdateOxygen();
        healthComponent.UpdateHealth();
    }

    void LateUpdate()
    {
        bool isSwimming = playerMovement.IsSwimming();
        animator.SetBool("isSwimming", isSwimming);
        if (isSwimming)
        {
            animator.Play("PlayerSwim");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FishEnemy")) 
        {
            Debug.Log("Player collided with enemy fish.");
            EnemyFishMovement enemyFish = other.GetComponent<EnemyFishMovement>();
            if (enemyFish != null)
            {
                StartCoroutine(enemyFish.ReturnToGuardSpotAndChase());
            }
        }
    }

    public void IncrementOxygenMultiplier()
    {
        oxygenMultiplier += 0.2f;
    }

    public void IncrementMoveSpeedMultiplier()
    {
        moveSpeedMultiplier += 0.2f;
    }

    public void IncrementBulletDamage()
    {
        baseBulletDamage += 5f;
    }

    public float GetMaxOxygenLevel()
    {
        return baseTank * oxygenMultiplier;
    }

    public float GetMaxHealth()
    {
        return healthComponent.maxHealth;
    }

    public float GetBulletDamage()
    {
        return baseBulletDamage;
    }

    public float GetMoveSpeed()
    {
        return baseMoveSpeed * moveSpeedMultiplier;
    }

    public int GetFishCount()
    {
        return fishCount;
    }

    public void SetFishCount(int count)
    {
        fishCount = count;
    }

    public int GetMoneyCount()
    {
        return moneyCount;
    }

    public void SetMoneyCount(int count)
    {
        moneyCount = count;
    }
}