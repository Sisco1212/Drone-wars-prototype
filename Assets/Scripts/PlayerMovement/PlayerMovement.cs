using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Ref and Vars")]
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
    PlayerControllerManager playerControllerManager;
    public int playerTeam;

    [Header("Health system")]
    public float maxHealth = 150.0f;
    public float currentHealth;
    public Slider healthBar;
    public GameObject uiCanvas;

    [Header("Photon vars")]
    PhotonView view;

    void Awake() {

        inputManager = GetComponent<InputManager>();
        // playerRb = GetComponent<Rigidbody>();
        droneRb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        view = GetComponent<PhotonView>();
        playerControllerManager = PhotonView.Find((int)view.InstantiationData[0]).GetComponent<PlayerControllerManager>();
        healthBar.minValue = 0f;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        // cameraGameobject = Camera.main.transform;
        // initialTargetFollowRotation = targetFollowGameObject.localRotation;
    }

    void Start()
    {
        if (!view.IsMine)
        {
            Destroy(droneRb);
            Destroy(uiCanvas);
        }
        
        if (view.Owner.CustomProperties.ContainsKey("Team"))
        {
            int team = (int)view.Owner.CustomProperties["Team"];
            playerTeam = team;
        }
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

    public void ApplyDamage(float damageValue)
    {
        view.RPC("RPC_TakeDamage", RpcTarget.All, damageValue);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {

         Debug.Log("RPC_TakeDamage called with damage: " + damage);
         
        if (!view.IsMine)
        return;

        currentHealth -= damage;
        healthBar.value = currentHealth;
        Debug.Log("Damage taken: " + damage);
        Debug.Log("Current health: " + currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerControllerManager.Die();
        ScoreBoard.Instance.PlayerDied(playerTeam);
    }

}
