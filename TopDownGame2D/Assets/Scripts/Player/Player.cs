using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public class Player : MonoBehaviour
{
    #region Movement Controls
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
    #endregion

    #region Action Controls
    private bool doingAction;

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

    // Cutting Control
    private bool _isCutting;
    public bool IsCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    // Digging Control
    private bool _isDigging;
    public bool IsDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    // Watering Control
    private bool _isWatering;
    public bool IsWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }
    #endregion

    // Player Items
    private PlayerItems playerItems;

    // Action Hadler Object
    private int handlingObj;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
    }

    // Initialize the player's variables
    private void Start()
    {
        initialSpeed = speed;
    }

    // Update the input variables of the game
    private void Update()
    {
        HandlingObject();

        OnInput();

        OnRun();
        OnRolling();
        OnCutting();
        OnDigging();
        OnWatering();
    }

    // Update the physics of the game
    private void FixedUpdate()
    {
        OnMove();
    }

    private void HandlingObject()
    {
        if (!Input.GetMouseButton(0))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 3;
            }
        }
    }

    #region Movements
    private void OnInput()
    {
        if (!doingAction)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    private void OnMove()
    {
        if (!doingAction)
        {
            rBody.MovePosition(rBody.position + _direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !doingAction)
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

    private void OnRolling()
    {
        // Right Mouse Button = 1
        if(Input.GetMouseButtonDown(1) && !doingAction)
        {
            _isRolling = true;
        }
        else
        {
            _isRolling = false;
        }
    }

    #endregion

    #region Actions
    private void OnCutting()
    {
        if (handlingObj == 1)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isCutting = true;
                doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isCutting = false;
                doingAction = false;
                speed = initialSpeed;
            }
        }
    }

    private void OnDigging()
    {
        if (handlingObj == 2)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isDigging = true;
                doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDigging = false;
                doingAction = false;
                speed = initialSpeed;
            }
        }
    }

    private void OnWatering()
    {
        if (handlingObj == 3)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0) && playerItems.TotalWater > 0f)
            {
                _isWatering = true;
                doingAction = true;
                speed = 0f;
            }

            if (IsWatering)
            {
                playerItems.TotalWater -= 0.01f;
            }


            if (Input.GetMouseButtonUp(0) || playerItems.TotalWater <= 0f)
            {
                _isWatering = false;
                doingAction = false;
                speed = initialSpeed;
            }
        }

    }
    #endregion
}
