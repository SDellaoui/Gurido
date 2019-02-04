using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    private void Start()
    {
        DisableComponents();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CmdAddDamage(10);
        }
    }
    private void DisableComponents()
    {
        if (!isLocalPlayer)
        {
            //Désactiver les components des autres joueurs sur notre instance
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.transform.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Projectile")
        {
            GameObject p = collision.collider.gameObject;
            if(p.GetComponent<ProjectileController>().GetOwner() != gameObject)
                CmdDestroyProjectile(collision.collider.gameObject);
        }
    }
    private void OnDisable()
    {
        sceneCamera.transform.gameObject.SetActive(true);
    }

    [Command]
    public void CmdAddDamage(int dmg)
    {
        gameObject.GetComponent<PlayerController>().AddDamage(dmg);
    }

    [Command]
    public void CmdAddHealth(int health)
    {
        gameObject.GetComponent<PlayerController>().AddHealth(health);
    }

    [Command]
    public void CmdDestroyItem(GameObject item)
    {
        NetworkServer.Destroy(item);
    }

    [Command]
    public void CmdShoot(GameObject projectile, Vector3 spawnPosition, float zRot, float speed)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
        GameObject bulletPrefab = gameObject.GetComponent<PlayerController>().weaponContainer.GetComponent<WeaponController>().bullet;
        //GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Weapons/Bullets/Bullet"),spawnPosition,rotation);

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, spawnPosition, rotation);
        bullet.GetComponent<ProjectileController>().SetBulletSpeed(speed);
        bullet.GetComponent<ProjectileController>().SetOwner(gameObject);
        Debug.Log(gameObject.name + " shot");
        NetworkServer.Spawn(bullet);

    }

    [Command]
    public void CmdDestroyProjectile(GameObject p)
    {
        NetworkServer.Destroy(p);
    }
}
