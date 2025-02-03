using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 

    private Transform drone;
    private Vector3 velocityCameraFollow;
    public Vector3 behindPosition = new Vector3(0, 2, -4);
    public float angle;
    private Vector2 lastTapPos;
    InputManager inputManager;

    void Awake()
    {
        drone = GameObject.FindGameObjectWithTag("Player").transform;
        inputManager = FindObjectOfType<InputManager>();
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         Vector2 curTapPos = Input.mousePosition;

    //         if(lastTapPos == Vector2.zero)
    //         {
    //             lastTapPos = curTapPos;
    //         }
    //    float delta = lastTapPos.x - curTapPos.x;
    //     lastTapPos = curTapPos;
        
    //     transform.Rotate(Vector3.up * delta);
    //     // currentYRotation = Mathf.SmoothDamp(currentYRotation, delta, ref rotationYVelocity, 0.25f);
        
    //     }
    
    // if (Input.GetMouseButtonUp(0))
    // {
    //     lastTapPos = Vector2.zero;
    // }

    // }


    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, drone.transform.TransformPoint(behindPosition) + Vector3.up * inputManager.verticalInput, ref velocityCameraFollow, 0.1f);
        transform.rotation = Quaternion.Euler(new Vector3(angle, drone.GetComponent<DroneMovement>().currentYRotation, 0));
    }

}
