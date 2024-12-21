using UnityEngine;

public abstract class PlayerItem : MonoBehaviour
{
    protected bool isEnabled = true;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;

    protected virtual void Start(){}
    public abstract void UseItem();
    public abstract void ToggleItem();
}
