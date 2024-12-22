using UnityEngine;

public class PlayerFisher : PlayerItem
{
    [SerializeField] private Material FisherDefaultMaterial;
    [SerializeField] private Material FisherGreenMaterial;
    [SerializeField] private GameObject scorePopupPrefab;
    private bool isCollidingWithFishFriendly = false;
    private GameObject collidingFishFriendly;
    public Player player;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (isEnabled)
        {
            RotateTowardsMouse();
        }

        if (isCollidingWithFishFriendly && Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
    }

    public override void ToggleItem()
    {
        isEnabled = !isEnabled;
        spriteRenderer.enabled = isEnabled;
        boxCollider.enabled = isEnabled;
    }

    public override void UseItem()
    {
        if (collidingFishFriendly != null)
        {
            collidingFishFriendly.GetComponent<FriendlyFishMovement>().ShrinkAndMoveTowardsPlayer();
            ShowScorePopup();
            player.IncrementFishCount();
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void ShowScorePopup()
    {
        if (scorePopupPrefab != null && collidingFishFriendly != null)
        {
            Instantiate(scorePopupPrefab, collidingFishFriendly.transform.position, Quaternion.identity);
            collidingFishFriendly = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FishFriendly"))
        {
            spriteRenderer.material = FisherGreenMaterial;
            isCollidingWithFishFriendly = true;
            collidingFishFriendly = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishFriendly"))
        {
            spriteRenderer.material = FisherDefaultMaterial;
            isCollidingWithFishFriendly = false;
        }
    }
}
