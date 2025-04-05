using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootingCooldown;
    [SerializeField] private Vector3 offset;
    private bool canShoot = true;
    private PlayerInputEventManager playerInputEventManager;
    private GameObject current_bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInputEventManager = GetComponent<PlayerInputEventManager>();
    }

    void OnEnable()
    {
        playerInputEventManager.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        playerInputEventManager.OnShoot -= HandleShoot;
    }
    private void HandleShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        current_bullet = Instantiate(bullet, transform.TransformPoint(offset), transform.rotation);
        current_bullet.GetComponent<GenericBullet>().owner = gameObject;
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }
}
