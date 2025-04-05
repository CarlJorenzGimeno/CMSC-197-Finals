using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private bool onCooldown = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject currentBullet;
    [SerializeField] private Color[] playerColors =
    {
        Color.blue,
        Color.red,
        Color.green,
    };

    private Rigidbody playerRB;
    private PlayerStates playerStates;
    private PlayerInputEventManager playerInputEventManager;
    private Vector2 currentMoveInput;

    private void Awake()
    {
        playerStates = GetComponent<PlayerStates>();
        playerInputEventManager = GetComponent<PlayerInputEventManager>();
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        Renderer playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.SetColor("_BaseColor", playerColors[playerInputEventManager.GetPlayerIndex()]);
        
        // Subscribe to input events
        playerInputEventManager.OnMove += HandleMoveInput;
        playerInputEventManager.OnJump += HandleJump;
        playerInputEventManager.OnAttack += HandleAttack;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (playerInputEventManager != null)
        {
            playerInputEventManager.OnMove -= HandleMoveInput;
            playerInputEventManager.OnJump -= HandleJump;
            playerInputEventManager.OnAttack -= HandleAttack;
        }
    }

    private void Update()
    {
        // Apply movement in Update for smooth motion
        if (currentMoveInput.sqrMagnitude > 0f)
        {
            Vector3 moveVector = new Vector3(currentMoveInput.x, 0, currentMoveInput.y);
            playerRB.MovePosition(transform.position + moveVector * speed * Time.fixedDeltaTime);
            playerRB.MoveRotation(Quaternion.LookRotation(moveVector, Vector3.up));
        }
    }

    private void HandleMoveInput(Vector2 moveInput)
    {
        currentMoveInput = moveInput;
    }

    private void HandleJump()
    {
        if (playerStates.IsOnGround)
        {
            playerStates.IsOnGround = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleAttack()
    {
        if (!onCooldown)
        {
            StartCoroutine(OnAttackCooldown());
        }
    }

    private void Shoot()
    {
        currentBullet = Instantiate(bullet, transform.position, transform.rotation);
    }

    private IEnumerator OnAttackCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }

    // Callbacks
    private int CountPlayers()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Number of players: " + players.Length);
        return players.Length;
    }
}