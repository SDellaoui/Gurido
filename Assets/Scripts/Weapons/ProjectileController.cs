using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileController : NetworkBehaviour
{
    [SyncVar]
    private float speed = 6f;

    [SyncVar]
    private GameObject owner;

    private string[] destroyColliders = { "Wall", "Breakable"};
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isClient)
            return;

        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //transform.position += transform.up * speed;
        Debug.DrawLine(transform.position, transform.position + transform.up);


    }
    public void SetBulletSpeed(float _speed)
    {
        speed = _speed;
    }

    public GameObject GetOwner() { return owner; }
    public void SetOwner(GameObject go)
    {
        owner = go;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(destroyColliders.Contains(collision.collider.gameObject.tag))
        {
            owner.GetComponent<PlayerNetworkSetup>().CmdDestroyProjectile(gameObject);
        }
    }
}
