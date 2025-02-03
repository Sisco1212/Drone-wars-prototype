using UnityEngine;

public class UsernameBillboard : MonoBehaviour
{
    Camera mainCam;

    // Update is called once per frame
    void Update()
    {
    if (mainCam == null)
    {
    mainCam = FindAnyObjectByType<Camera>();    
    }

    if (mainCam == null)
    return;

    transform.LookAt(mainCam.transform);
    transform.Rotate(Vector3.up * 180);
    }
}
