using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed;
        Debug.DrawLine(transform.position, transform.position + transform.up);
    }
    public void SetBulletSpeed(float _speed)
    {
        speed = _speed;
    }

}
