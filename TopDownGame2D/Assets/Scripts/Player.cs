using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public class Player : MonoBehaviour
{
    // Player's Rigidbody 
    private Rigidbody2D rBody;

    // Speed
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    private float initialSpeed;

    // Direction Control
    private Vector2 _direction;
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    // Running Control
    private bool _isRunning;
    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    // Running Control
    private bool _isRolling;
    public bool IsRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    // Initialize the player's variables
    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    // Update the input variables of the game
    private void Update()
    {
        OnInput();

        OnRun();

        OnRoll();
    }

    // Update the physics of the game
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    private void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void OnMove()
    {
        rBody.MovePosition(rBody.position + _direction * speed * Time.fixedDeltaTime);
    }

    private void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    private void OnRoll()
    {
        // Right Mouse = 1
        if(Input.GetMouseButtonDown(1))
        {
            _isRolling = true;
        }
        else
        {
            _isRolling = false;
        }
    }

    #endregion
}
