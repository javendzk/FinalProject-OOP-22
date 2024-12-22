using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator; 
    
    public HealthBar healthBar;
    public OxygenBar oxygenBar;
    public float maxHealth = 100; //jadi public
    public float currentHealth;
    public float maxOxygen = 100; //jadi public
    public float currentOxygen;
    public float oxygenDepletionRate = 1.0f;
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
        currentHealth = maxHealth; //full hp at start
        healthBar.SetMaxHealth(maxHealth);
        currentOxygen = maxOxygen; //full oxygen at start
        oxygenBar.SetMaxOxygen(maxOxygen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10.5f);
        }

        // Decrease currentOxygen over time
    currentOxygen -= oxygenDepletionRate * Time.deltaTime;
    if (currentOxygen < 0)
    {
        currentOxygen = 0;
    }
    oxygenBar.SetOxygen(currentOxygen);
    }

    void FixedUpdate()
    {
        playerMovement.Move();
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
        return maxHealth;
    }

    public float GetBulletDamage()
    {
        return baseBulletDamage;
    }

    public float GetMoveSpeed()
    {
        return baseMoveSpeed * moveSpeedMultiplier;
    }
void TakeDamage (float damage) //demo take damage
    {
        currentHealth -= (int)damage;

        healthBar.SetHealth(currentHealth);
    }
}