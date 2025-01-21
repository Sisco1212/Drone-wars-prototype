using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public float bulletForce = 4000.0f;
    public GameObject bulletHole_prefab;
    public float distanceFromTheWall = 0.15f;
    
    void Awake()
    {
    gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce);
    }

    void FixedUpdate()
    {
    BulletLife();
    }

    void BulletLife()
    {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;
    if(Physics.Raycast(ray, out hit, 1000.0f))
    {
    if (hit.distance < 3)
    {
        Vector3 position = hit.point + (hit.normal * distanceFromTheWall);
        Vector3 lookRotation = hit.normal;
        Instantiate(bulletHole_prefab, position, Quaternion.LookRotation(lookRotation));
        Destroy(gameObject);
    }
    }
    Destroy(gameObject, 4.0f);
    }
}
