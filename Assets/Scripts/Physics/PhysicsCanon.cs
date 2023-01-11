using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCanon : MonoBehaviour
{
    // Editor parameters
    public GameObject bulletPrefab;
    public float speed;

    public void Shoot(Vector3 clickPosition)
    {
        Vector3 direction = Vector3.Normalize(clickPosition - transform.position);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.velocity = speed * direction;
    }
}
