using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputEventManager : MonoBehaviour
{
    private PlayerInput input;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private Vector2 _moveInput;
    private bool _jumpInput;
    private bool _shootInput;
    private bool _attackInput;

    // Events for input actions
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnJumpReleased;
    public event Action OnShoot;
    public event Action OnShootReleased;
    public event Action OnAttack;
    public event Action OnAttackReleased;

    private void Awake()
    {
        input = this.GetComponent<PlayerInput>();
        inputAsset = input.actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    // Keeping the getters for backward compatibility
    public Vector2 GetMoveInput() => _moveInput;
    public bool GetJumpInput() => _jumpInput;
    public bool GetShootInput() => _shootInput;
    public bool GetAttackInput() => _attackInput;

    // Modified setters that trigger events
    public void SetMoveInput(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        OnMove?.Invoke(_moveInput);
    }

    public void SetJumpInput(InputAction.CallbackContext ctx)
    {
        bool newValue = ctx.ReadValue<float>() > 0f;
        
        // Only trigger events when the value changes
        if (newValue != _jumpInput)
        {
            _jumpInput = newValue;
            
            if (_jumpInput)
                OnJump?.Invoke();
            else
                OnJumpReleased?.Invoke();
        }
    }

    public void SetShootInput(InputAction.CallbackContext ctx)
    {
        bool newValue = ctx.ReadValue<float>() > 0f;
        
        if (newValue != _shootInput)
        {
            _shootInput = newValue;
            
            if (_shootInput)
                OnShoot?.Invoke();
            else
                OnShootReleased?.Invoke();
        }
    }

    public void SetAttackInput(InputAction.CallbackContext ctx)
    {
        bool newValue = ctx.ReadValue<float>() > 0f;
        
        if (newValue != _attackInput)
        {
            _attackInput = newValue;
            
            if (_attackInput)
                OnAttack?.Invoke();
            else
                OnAttackReleased?.Invoke();
        }
    }

    public int GetPlayerIndex()
    {
        return input.playerIndex;
    }
}