using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public enum Orientation
{
    LEFT,
    RIGHT
}

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

    private Orientation _orientation;
    public Orientation Orientation
    {
        get { return _orientation; }
        set { _orientation = value; }
    }
    #endregion

    #region Action Controls
    private bool _doingAction;
    public bool DoingAction
    {
        get { return _doingAction; }
        set { _doingAction = value; }
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

    // Fishing Control
    private bool _isFishing;
    public bool IsFishing
    {
        get { return _isFishing; }
        set { _isFishing = value; }
    }

    // Crafting Control
    private bool _isCrafting;
    public bool IsCrafting
    {
        get { return _isCrafting; }
        set { _isCrafting = value; }
    }
    #endregion

    // Life
    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    // Player Items
    private PlayerItems playerItems;

    // Tools Hadler Object
    public bool hasHandledObj;
    private int _handlingObj;
    public int HandlingObj
    {
        get { return _handlingObj; }
        set { _handlingObj = value; }
    }

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
        OnFishing();
        OnCrafting();
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
                _handlingObj = 1;
                hasHandledObj = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _handlingObj = 2;
                hasHandledObj = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _handlingObj = 3;
                hasHandledObj = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _handlingObj = 4;
                hasHandledObj = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _handlingObj = 5;
                hasHandledObj = true;
            }
        }
    }

    #region Movements
    private void OnInput()
    {
        if (!_doingAction)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    private void OnMove()
    {
        if (!_doingAction)
        {
            rBody.MovePosition(rBody.position + _direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !_doingAction)
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
        if(Input.GetMouseButtonDown(1) && !_doingAction)
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
        if (_handlingObj == 1)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isCutting = true;
                _doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isCutting = false;
                _doingAction = false;
                speed = initialSpeed;
            }
        }
    }

    private void OnDigging()
    {
        if (_handlingObj == 2)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isDigging = true;
                _doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDigging = false;
                _doingAction = false;
                speed = initialSpeed;
            }
        }
    }

    private void OnWatering()
    {
        if (_handlingObj == 3)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0) && playerItems.TotalWater > 0f)
            {
                _isWatering = true;
                _doingAction = true;
                speed = 0f;
            }

            if (IsWatering)
            {
                playerItems.TotalWater -= 0.01f;
            }


            if (Input.GetMouseButtonUp(0) || playerItems.TotalWater <= 0f)
            {
                _isWatering = false;
                _doingAction = false;
                speed = initialSpeed;
            }
        }

    }

    private void OnFishing()
    {
        if (_handlingObj == 4)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isFishing = true;
                _doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isFishing = false;
                _doingAction = false;
                speed = initialSpeed;
            }
        }
    }

    private void OnCrafting()
    {
        if (_handlingObj == 5)
        {
            // Left Mouse Button = 0
            if (Input.GetMouseButtonDown(0))
            {
                _isCrafting = true;
                _doingAction = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isCrafting = false;
                _doingAction = false;
                speed = initialSpeed;
            }
        }
    }
    #endregion
}
