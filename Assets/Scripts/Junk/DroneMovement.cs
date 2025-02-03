using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
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
    // private AudioSource droneSound;
    private Vector2 lastTapPos;
    private float delta;

    void Awake()
    {
        droneRb = GetComponent<Rigidbody>();
        // droneSound = gameObject.transform.Find("Drone_sound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if(lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }
        delta = lastTapPos.x - curTapPos.x;
        lastTapPos = curTapPos;

        // if (lastTapPos.x < 1.0f)
        // {
        //     wantedYRotation -= rotateAmountByKeys;
        // }
        // if (lastTapPos.x > 1.0f)
        // {
        //     wantedYRotation += rotateAmountByKeys;
        // }
        // currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

        // currentYRotation = Mathf.SmoothDamp(currentYRotation, delta, ref rotationYVelocity, 0.25f);
        
        }
    
    if (Input.GetMouseButtonUp(0))
    {
        lastTapPos = Vector3.zero;
    }

    }

    void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerve();
        // DroneSound();
        droneRb.AddRelativeForce(Vector3.up * upForce);
        droneRb.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );
    }

    void MovementUpDown()
    {
        if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f))
        {
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
        {
        droneRb.linearVelocity = droneRb.linearVelocity;
        }

        if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)){
            droneRb.linearVelocity = new Vector3(droneRb.linearVelocity.x, Mathf.Lerp(droneRb.linearVelocity.y, 0, Time.deltaTime * 5), droneRb.linearVelocity.z);
            upForce = 170;
        }    
        if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)))
        {
            droneRb.linearVelocity = new Vector3(droneRb.linearVelocity.x, Mathf.Lerp(droneRb.linearVelocity.y, 0, Time.deltaTime * 5), droneRb.linearVelocity.z);
            upForce = 100;
        }
        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
        {
            upForce = 210;
        }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        if(Input.GetKey(KeyCode.I))
        {
          upForce = 300;
          if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
          {
            upForce = 300;
          }
        }
        else if(Input.GetKey(KeyCode.L))
        {
            upForce = -100;
        }
        else if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.L) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
            // transform.position = transform.position;
        }
    }

    void MovementForward()
    {
    if (Input.GetAxis("Vertical") != 0)
    {
        droneRb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
        tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 10 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
    }
    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.K))
        {
            wantedYRotation += rotateAmountByKeys;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }

    void ClampingSpeedValues()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5));
        }

        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 10.0f, Time.deltaTime * 5));
        }

        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
        droneRb.linearVelocity = Vector3.ClampMagnitude(droneRb.linearVelocity, Mathf.Lerp(droneRb.linearVelocity.magnitude, 5.0f, Time.deltaTime * 5));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2 && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            droneRb.linearVelocity = Vector3.SmoothDamp(droneRb.linearVelocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    void Swerve()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
        droneRb.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
        tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -10 * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);
        }
        else {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
        }
    }

    // void DroneSound()
    // {
    //     droneSound.pitch = 1 + (droneRb.velocity.magnitude / 100);
    // }
}
