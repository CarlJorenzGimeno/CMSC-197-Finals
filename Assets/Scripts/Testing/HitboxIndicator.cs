using UnityEngine;
using System.Collections;

public class HitboxIndicator : MonoBehaviour
{
    private Renderer cachedRenderer;
    private PlayerInputEventManager playerInputEventManager;

    void Start()
    {
        cachedRenderer = GetComponent<Renderer>();
        playerInputEventManager = transform.parent.GetComponent<PlayerInputEventManager>();
        
        // Subscribe to the attack event
        playerInputEventManager.OnAttack += ExecuteAttack;
    }

    void OnDestroy()
    {
        // Unsubscribe when destroyed to prevent memory leaks
        if (playerInputEventManager != null)
            playerInputEventManager.OnAttack -= ExecuteAttack;
    }

    private void ExecuteAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        EnableHitboxes();
        yield return new WaitForSeconds(1f);
        DisableHitboxes();
    }

    public void EnableHitboxes()
    {
        cachedRenderer.material.SetColor("_BaseColor", Color.red);
    }

    public void DisableHitboxes()
    {
        cachedRenderer.material.SetColor("_BaseColor", Color.blue);
    }
}