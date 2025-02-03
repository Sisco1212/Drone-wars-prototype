using UnityEngine;
using Photon.Pun;

public class ShootingControls : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;
    PhotonView view;
    
    [Header("Shooting vars")]
    public float fireRate = 0f;
    public float fireRange = 100f;
    public float fireDamage = 15f;
    public float nextFireTime = 0f;
    public Transform firePoint;
    public bool isShooting;

    [Header("Reload")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    [Header("Sound effects")]
    private AudioSource audioSource;
    public AudioClip shootingSound;
    public AudioClip reloadSound;
    public LayerMask playerLayer;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleFlash2;

    void Start() {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        view = GetComponent<PhotonView>();
    }


    void Update()
    {
        if(!view.IsMine)
        {
            return;
        }
        if (inputManager.reloadInput && currentAmmo < maxAmmo)
        {
            Reload();
        }
        isShooting = inputManager.fireInput;

        if (isShooting)
        {
            if (Time.time >= nextFireTime)
            {
            nextFireTime = Time.time + 1f/fireRate;
            Shoot();
            }
        }
    }

    private void Shoot() 
    {
            if(muzzleFlash != null && muzzleFlash2 != null)
    {
        muzzleFlash.Play();
        muzzleFlash2.Play();
    }
    else {
        Debug.Log("No muzzle flash");
    }
    if(audioSource != null)
    {
        audioSource.clip = shootingSound;
        audioSource.Play();
    }
    if(currentAmmo > 0)
    {
    RaycastHit hit;
    if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange, playerLayer))
    {
    
    Debug.Log(hit.transform.name);
    Vector3 hitPoint = hit.point;
    Vector3 hitNormal = hit.normal;

    PlayerMovement playerMovementDamage = hit.collider.GetComponentInParent<PlayerMovement>();

    if (playerMovementDamage != null)
    {
        Debug.Log("PlayerMovement component found on: " + hit.transform.name);
        playerMovementDamage.ApplyDamage(fireDamage);
    }
    else {
        Debug.Log("PlayerMovement component not found on: " + hit.transform.name);
    }
    }
        Debug.DrawRay(firePoint.position, firePoint.forward * fireRange, Color.red, 2.0f);

    currentAmmo--;
    }
    else {
        Reload();
    }
    }

    private void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;
                if(audioSource != null)
    {
        audioSource.clip = reloadSound;
        audioSource.PlayOneShot(reloadSound);
    }
            Invoke("FinishReload", reloadTime);
        }
    }

    private void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
