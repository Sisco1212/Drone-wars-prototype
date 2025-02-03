using Photon.Pun;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;

    PhotonView view;

    void Awake() {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        view = GetComponent<PhotonView>();
    }

    void Start()
    {
         if (!view.IsMine)
         Destroy(GetComponentInChildren<CameraManage>().gameObject);
    }

    void Update() {
        if (!view.IsMine)
           return;
        inputManager.HandleAllInputs();
    }

    void FixedUpdate() 
    {
        if (!view.IsMine)
        return;
        playerMovement.HandleAllMovements();
    }
}
