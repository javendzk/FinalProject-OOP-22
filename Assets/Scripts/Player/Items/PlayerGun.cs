using UnityEngine;
using System.Collections;

public class PlayerGun : PlayerItem
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite greenSprite;
    private bool isReloading = false;
    private float reloadTime = 0.5f;
    private Transform bulletSpawnPoint;
    private Player player;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = defaultSprite;
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (isEnabled)
        {
            RotateTowardsMouse();
        }

        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            UseItem();
        }
    }

    public override void ToggleItem()
    {
        isEnabled = !isEnabled;
        spriteRenderer.enabled = isEnabled;
    }

    public override void UseItem()
    {
        if (!isReloading)
        {
            Shoot();
            StartCoroutine(Reload());
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            GunBullet bulletComponent = bullet.GetComponent<GunBullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(player.GetBulletDamage());
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        spriteRenderer.sprite = defaultSprite;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        spriteRenderer.sprite = greenSprite;
    }

    void OnEnable()
    {
        if (isReloading)
        {
            StartCoroutine(Reload());
        }
    }
}
