// using UnityEngine;
// using Cinemachine;

// [RequireComponent(typeof(CinemachineFreeLook))]

// public class CameraLook : MonoBehaviour
// {

//     CinemachineFreeLook cinemachine;
//     InputManager inputManager;
//     [SerializeField]
//     private float lookSpeed = 1f;

//     void Start()
//     {
//         cinemachine = GetComponent<CinemachineFreeLook>();
//         inputManager = FindObjectOfType<InputManager>();
//     }


//     void Update()
//     {
//         Vector2 delta = inputManager.PlayerControls.PlayerMovement.Look.ReadValue<Vector2>();
//         cinemachine.m_XAxis.value += delta.x * 200 * lookSpeed * Time.deltaTime;
//         cinemachine.m_YAxis.value += delta.y * lookSpeed * Time.deltaTime;
//     }
// }
