using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePhysicSpawn : MonoBehaviour
{
    Rigidbody2D rb;
    public float thrust = 1f;
    private float forceTime = 0.5f;
    private float initForceTime = 0f;
    private int nBounces = 2;
    private int bounceCount = 0;

    //Force
    private float bounceThrust;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 startPoint = new Vector2(0, 0);
        Vector2 endPoint = new Vector2(3f, 3f);

        transform.position = startPoint;
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(0.2f, 1f);
        rb.velocity = direction * thrust;
        bounceThrust = thrust;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(initForceTime < forceTime)
        {
            initForceTime += Time.deltaTime;

            //rb.velocity = new Vector2(0.5f, 1f) * thrust;
        }
        if(transform.position.y < 0 && bounceCount < nBounces)
        {
            bounceCount += 1;
            bounceThrust /= 2;

            rb.velocity = direction * bounceThrust;
            if (bounceCount == nBounces)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            bounceThrust = thrust;
            rb.velocity = direction * thrust;

            bounceCount = 0;
        }
    }
}
