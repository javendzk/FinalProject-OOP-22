using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 2;
    [SerializeField] private float blinkInterval = 0.05f;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private GameObject playerVisual;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Start()
    {
        if (playerVisual != null)
        {
            spriteRenderer = playerVisual.GetComponent<SpriteRenderer>();
        }

        else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;
    }

    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(Blink());
        }
    }
}
