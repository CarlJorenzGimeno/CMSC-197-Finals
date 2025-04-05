using UnityEngine;
using System.Collections;
using TMPro;

public class Reflect : MonoBehaviour
{
    [SerializeField] private float parryWindow = 0.5f;
    [SerializeField] private float parryCooldown = 3f;
    private bool _parryActive = false;
    private bool _parryReady = true;
    private PlayerInputEventManager playerInputEventManager;
    private TMPro.TextMeshProUGUI gui;
 
    private void Awake()
    {
        playerInputEventManager = transform.parent.GetComponent<PlayerInputEventManager>();
    }
    private void OnEnable()
    {
        playerInputEventManager.OnAttack += HandleParry;
    }
    private void OnDisable()
    {
        playerInputEventManager.OnAttack -= HandleParry;
    }
    void Start()
    {
        gui = GameObject.Find("INFO").GetComponent<TextMeshProUGUI>();
    }

    private void HandleParry()
    {
        if (_parryReady)
        {
            _parryActive = true;
            _parryReady = false;
            gui.text = "Parry Not Ready";
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
            StartCoroutine(ParryAttack());
            StartCoroutine(ParryCooldown());
        }
    }

    private IEnumerator ParryAttack()
    {
        yield return new WaitForSeconds(parryWindow);
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.clear);
        _parryActive = false;
    }

    private IEnumerator ParryCooldown()
    {
        yield return new WaitForSeconds(parryCooldown);
        _parryReady = true;
        gui.text = "Parry Ready";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && _parryActive)
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            otherRB.linearVelocity *= -1;
        }
    }
}
