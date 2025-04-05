using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerStates states;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        states = GetComponent<PlayerStates>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (other.CompareTag("Player") && obj.CompareTag("Bullet"))
        {
            // states.IsAlive = false;
            // Destroy(obj);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            states.IsOnGround = true;
        }
    }
}
