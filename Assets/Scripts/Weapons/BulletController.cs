using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    private float speed = 0f;
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
        transform.position += transform.up * speed;
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
            Destroy(gameObject);
        }
    }
}
