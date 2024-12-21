using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Material CursorDefaultMaterial;
    public Material CursorGreenMaterial;
    public GameObject scorePopupPrefab;
    private bool isEnabled = true;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isCollidingWithFishFriendly = false;
    private GameObject collidingFishFriendly;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEnabled = !isEnabled;
            spriteRenderer.enabled = isEnabled;
            boxCollider.enabled = isEnabled;
        }

        if (isEnabled)
        {
            RotateTowardsMouse();
        }

        if (isCollidingWithFishFriendly && Input.GetMouseButtonDown(0))
        {
            collidingFishFriendly.GetComponent<FriendlyFishMovement>().ShrinkAndMoveTowardsPlayer();
            ShowScorePopup();
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
            GameObject popup = Instantiate(scorePopupPrefab, collidingFishFriendly.transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FishFriendly"))
        {
            spriteRenderer.material = CursorGreenMaterial;
            isCollidingWithFishFriendly = true;
            collidingFishFriendly = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishFriendly"))
        {
            spriteRenderer.material = CursorDefaultMaterial;
            isCollidingWithFishFriendly = false;
            collidingFishFriendly = null;
        }
    }
}
