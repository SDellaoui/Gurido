using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode { Single, Automatic };

public class WeaponController : MonoBehaviour
{
    public BulletController bullet;
    public float bulletSpeed;
    public float fireRate;
    public FireMode fireMode;

    [SerializeField]
    private Transform bulletSpawnPosition = null;
    private bool isShooting = false;
    float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShooting();
        
    }

    void UpdateShooting()
    {
        if(Input.GetAxisRaw("Fire1") == 1f & isShooting == false)
        {
            Shoot();
            isShooting = true;
        }
        else if (Input.GetAxisRaw("Fire1") < 1f)
            isShooting = false;

        //Automatic reshoot based on firerate if automatic mode is active
        if (fireMode == FireMode.Automatic)
        {
            if (isShooting)
            {
                shootTime += Time.deltaTime;
                if (shootTime >= fireRate)
                {
                    Shoot();
                    shootTime = 0f;
                }
            }
        }
    }

    void Shoot()
    {
        BulletController b = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z))) as BulletController;
        b.SetBulletSpeed(bulletSpeed);
        b.SetOwner(transform.parent.gameObject);
    }
}
