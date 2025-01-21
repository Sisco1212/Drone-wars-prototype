using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    // Vector3 moveDirection;
    // Transform cameraGameobject;
    // Rigidbody playerRb;
    // float movementSpeed = 1f;
    float rotationSpeed = 13f;
    
    Rigidbody droneRb;
    public float upForce;
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward;
    private float tiltVelocityForward;
    private float wantedYRotation;
   [HideInInspector] public float currentYRotation;
    private float rotateAmountByKeys = 1.5f;
    private float rotationYVelocity;
    private Vector3 velocityToSmoothDampToZero;
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    // public Transform targetFollowGameObject;
    // public Transform child;
    // private Quaternion initialTargetFollowRotation;

    void Awake() {
        inputManager = GetComponent<InputManager>();
        // playerRb = GetComponent<Rigidbody>();
        droneRb = GetComponent<Rigidbody>();
        // cameraGameobject = Camera.main.transform;
        // initialTargetFollowRotation = targetFollowGameObject.localRotation;
    }

    public void HandleAllMovements()
    {
        // HandleMovement();
        // HandleRotation();
        // MovementUpDown();
        MovementForward();
        // Rotation();
        ClampingSpeedValues();
        Swerve();
    }

    // void HandleMovement() {
    //     moveDirection = cameraGameobject.forward * inputManager.verticalInput;
    //     moveDirection = moveDirection + cameraGameobject.right * inputManager.horizontalInput;
    //     moveDirection.Normalize();
    //     moveDirection.y = 0;

    //     Vector3 movementVelocity = moveDirection;
    //     playerRb.linearVelocity = movementVelocity;
    // }

    // void Update()
    // {
    //     targetFollowGameObject.localRotation = initialTargetFollowRotation;
    // }

    //     void MovementUpDown()
    // {
    //     if ((Mathf.Abs(inputManager.verticalInput) > 0.2f || Mathf.Abs(inputManager.horizontalInput) > 0.2f))
    //     {
    //     if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
    //     {
    //     droneRb.linearVelocity = droneRb.linearVelocity;
    //     }

    //     if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)){
    //         droneRb.linearVelocity = new Vector3(droneRb.linearVelocity.x, Mathf.Lerp(droneRb.linearVelocity.y, 0, Time.deltaTime * 5), droneRb.linearVelocity.z);
    //         upForce = 170;
    //     }    
    //     if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)))
    //     {
    //         droneRb.linearVelocity = new Vector3(droneRb.linearVelocity.x, Mathf.Lerp(droneRb.linearVelocity.y, 0, Time.deltaTime * 5), droneRb.linearVelocity.z);
    //         upForce = 100;
    //     }
    //     if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
    //     {
    //         upForce = 210;
    //     }
    //     }

    //     if (Mathf.Abs(inputManager.verticalInput) < 0.2f && Mathf.Abs(inputManager.horizontalInput) > 0.2f)
    //     {
    //         upForce = 135;
    //     }

    //     if(Input.GetKey(KeyCode.I))
    //     {
    //       upForce = 300;
    //       if (Mathf.Abs(inputManager.horizontalInput) > 0.2f)
    //       {
    //         upForce = 300;
    //       }
    //     }
    //     else if(Input.GetKey(KeyCode.L))
    //     {
    //         upForce = -100;
    //     }
    //     else if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.L) && (Mathf.Abs(inputManager.verticalInput) < 0.2f && Mathf.Abs(inputManager.horizontalInput) < 0.2f))
    //     {
    //         upForce = 98.1f;
    //         // transform.position = transform.position;
    //     }
    // }

    
    void MovementForward()
    {
    if (inputManager.verticalInput != 0)
    {
        droneRb.AddRelativeForce((Vector3.forward * inputManager.verticalInput * movementForwardSpeed));
        tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 10 * inputManager.verticalInput, ref tiltVelocityForward, 0.1f);
        // transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
    }


    // void HandleRotation() {
    //     Vector3 targetDirection = Vector3.zero;

    //     targetDirection = cameraGameobject.forward * inputManager.verticalInput;
    //     targetDirection = targetDirection + cameraGameobject.right * inputManager.horizontalInput;
    //     targetDirection.Normalize();
    //     targetDirection.y =0;

    //     if (targetDirection == Vector3.zero)
    //     {
    //         targetDirection = transform.forward;
    //     }

    //    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
    //    Quaternion playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    //    transform.rotation = playerRotation;
    // }


    // void Rotation()
    // {
    //     // if (Input.GetKey(KeyCode.J))
    //     // {
    //     //     wantedYRotation -= rotateAmountByKeys;
    //     // }
    //     // if (Input.GetKey(KeyCode.K))
    //     // {
    //     //     wantedYRotation += rotateAmountByKeys;
    //     // }
    //     // currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    //     Quaternion rotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z));
    //     transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    // }

    void LateUpdate()
    {
        // Rotation();
        MovementForward();
    }

    void ClampingSpeedValues()
    {
        if(Mathf.Abs(inputManager.verticalInput) > 0.2f && Mathf.Abs(inputManager.horizontalInput) > 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5));
        }

        if(Mathf.Abs(inputManager.verticalInput) > 0.2f && Mathf.Abs(inputManager.horizontalInput) < 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5));
        }

        if(Mathf.Abs(inputManager.verticalInput) > 0.2f && Mathf.Abs(inputManager.horizontalInput) > 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 5.0f, Time.deltaTime * 5));
        }
        if(Mathf.Abs(inputManager.verticalInput) < 0.2 && Mathf.Abs(inputManager.horizontalInput) < 0.2f)
        {
            droneRb.linearVelocity = Vector3.SmoothDamp(droneRb.linearVelocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    void Swerve()
    {
        if (Mathf.Abs(inputManager.horizontalInput) > 0.2f)
        {
        droneRb.AddRelativeForce(Vector3.right * inputManager.horizontalInput * sideMovementAmount);
        tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -10 * inputManager.horizontalInput, ref tiltAmountVelocity, 0.1f);
        }
        else {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
        }
    }
}
