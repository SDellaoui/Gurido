using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType
{
    Money,
    Health
}

public class CollectibleController : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectibleType collectibleType;
    public int collectibleValue;

    private float _xDir;
    private float _yDir;

    //Direction
    private Vector2 direction;
    //Speed
    private float speed = 1f;
    private float moveTime = 1f;
    private float initMoveTime = 0f;
    void Start()
    {
        _xDir = Random.Range(-1f, 1f);
        _yDir = Random.Range(-1f, 1f);

        direction = new Vector2(_xDir - transform.position.x, _yDir - transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (initMoveTime < moveTime)
        {
            transform.Translate(new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0));
            initMoveTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            switch (collectibleType)
            {
                case CollectibleType.Health:
                    collision.collider.gameObject.GetComponent<PlayerController>().AddHealth(collectibleValue);
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
