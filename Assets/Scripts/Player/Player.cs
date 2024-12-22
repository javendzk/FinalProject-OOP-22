using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private HealthComponent healthComponent;
    private OxygenComponent oxygenComponent;

    [SerializeField] private int fishCount;
    [SerializeField] private int moneyCount;
    [SerializeField] private float oxygenMultiplier = 1f;
    [SerializeField] private float moveSpeedMultiplier = 1f;
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float baseTank = 150f;
    [SerializeField] private float baseBulletDamage = 10f;
    [SerializeField] public bool boughtGun;

    public event Action<int> OnFishCountChanged;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("PlayerVisual").GetComponent<Animator>();
        healthComponent = GetComponent<HealthComponent>();
        oxygenComponent = GetComponent<OxygenComponent>();
    }

    void Update()
    {
        if (oxygenComponent != null || healthComponent != null)
        {
            oxygenComponent.UpdateOxygen();
            healthComponent.UpdateHealth();
            if (healthComponent.currentHealth == 0 || oxygenComponent.currentOxygen == 0)
            {
                Die();
            }
        }

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
        OnFishCountChanged?.Invoke(fishCount);
    }

    public int GetMoneyCount()
    {
        return moneyCount;
    }

    public void SetMoneyCount(int count)
    {
        moneyCount = count;
    }

    public void IncrementFishCount()
    {
        fishCount++;
        
    }

    private void Die()
    {
        fishCount = 0;
        GameManager.Instance.LevelManager.LoadScene("Dead");
    }
}