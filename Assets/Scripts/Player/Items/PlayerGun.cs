using UnityEngine;

public class PlayerGun : PlayerItem
{

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    public override void ToggleItem()
    {
        isEnabled = !isEnabled;
        spriteRenderer.enabled = isEnabled;
    }

    public override void UseItem()
    {
        // Implement the logic for using the gun
    }
}
