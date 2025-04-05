using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] private bool _isAlive;
    [SerializeField] private bool _isOnGround;
    [SerializeField] private int _playerID;
    public bool IsAlive { get => _isAlive; set => _isAlive = value;}
    public bool IsOnGround { get => _isOnGround; set => _isOnGround = value; }

    private void Awake()
    {
        _playerID = GameObject.FindGameObjectsWithTag("Player").Length;
    }
}
