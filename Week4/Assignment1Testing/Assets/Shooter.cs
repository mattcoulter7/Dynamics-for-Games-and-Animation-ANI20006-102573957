using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSource;
    public float bulletVelocity = 500f;
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.transform.position = bulletSource.position;
        Ray bulletRay = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        bullet.GetComponent<Rigidbody>().AddForce(bulletRay.direction * bulletVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }
}
