using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private bool isEnabled = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEnabled = !isEnabled;
            spriteRenderer.enabled = isEnabled;
        }

        if (isEnabled)
        {
            RotateTowardsMouse();
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

}
