using UnityEngine;

public class InputManager : MonoBehaviour
{

    PlayerControls playerControls;
    public Vector2 movementInput;
    public Vector2 rotationInput;
    public float verticalInput;
    public float horizontalInput;
    public float rotationVerticalInput;
    public float rotationHorizontalInput;
    public bool fireInput;
    public bool reloadInput;
    public bool scopeInput;


   // void Awake() 
   // {
   //    playerControls.PlayerMovement.Movement.canceled += i => movementInput = Vector2.zero;
   // }

    void OnEnable() 
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); 
            playerControls.PlayerMovement.Look.performed += i => rotationInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Fire.performed += i => fireInput = true;
            playerControls.PlayerActions.Fire.canceled += i => fireInput = false;
            playerControls.PlayerActions.Reload.performed += i => reloadInput = true;
            playerControls.PlayerActions.Scope.performed += i => scopeInput = true;
            playerControls.PlayerActions.Scope.canceled += i => scopeInput = false;
        }

        playerControls.Enable();
    }

     void OnDisable()
     {
        playerControls.Disable();
     }

     public void HandleAllInputs()
     {
        HandleMovementInput();
        HandleRotationInput();
     }

     void HandleMovementInput()
     {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
     }

     void HandleRotationInput()
     {
        rotationVerticalInput = rotationInput.y;
        rotationHorizontalInput = rotationInput.x;
     }

     void FixedUpdate()
     {
      playerControls.PlayerMovement.Movement.canceled += i => movementInput = Vector2.zero;
      playerControls.PlayerMovement.Look.canceled += i => rotationInput = Vector2.zero;
     }
}
