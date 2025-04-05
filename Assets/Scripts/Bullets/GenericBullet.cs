using UnityEngine;
using System.Collections;

public class GenericBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lifetime;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject _owner;

    public GameObject owner { get => _owner; set => _owner = value;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }
}
