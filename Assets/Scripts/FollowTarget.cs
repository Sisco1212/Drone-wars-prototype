using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField]
    private Transform followTarget;
    [SerializeField]
    private float rotationalSpeed = 10f;
    [SerializeField]
    private float bottomClamp = -40f;
    [SerializeField]
    private float topClamp = 70f;

    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;

    InputManager inputManager;

    void Awake() 
    {
        inputManager = FindFirstObjectByType<InputManager>();
    }

    void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        float mouseX = inputManager.rotationHorizontalInput;
        float mouseY = inputManager.rotationVerticalInput;

        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotations(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private void ApplyRotations(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis) {

    // if (axis == "Horizontal")
    // {
    //     return inputManager.horizontalInput * rotationalSpeed * Time.deltaTime;
    // }
    // else if (axis == "Vertical")
    // {
    //     return inputManager.verticalInput * rotationalSpeed * Time.deltaTime;
    // }
    // return 0f;
    // }

    return Input.GetAxis(axis) * rotationalSpeed * Time.deltaTime;
}
}