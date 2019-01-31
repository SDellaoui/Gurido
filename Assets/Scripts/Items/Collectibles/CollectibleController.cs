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
    private float _maxDistance;

    //Direction
    private Vector2 initPosition;
    private Vector2 direction;

    //Speed
    private float speed = 5f;
    private float moveTime = 1f;
    private float initMoveTime = 0f;
    private bool freezePosition = false;

    void Start()
    {
        _xDir = Random.Range(-1f, 1f);
        _yDir = Random.Range(-1f, 1f);

        float _maxDistance = 20 + Random.Range(0f,1f);
        Debug.Log(_maxDistance);

        initPosition = transform.position;

        direction = new Vector2(_xDir * transform.position.x, _yDir * transform.position.y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (initMoveTime < moveTime)
        {
            transform.Translate(new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0));
            initMoveTime += Time.deltaTime;

            float _distanceFromInit = Vector2.Distance(transform.position, initPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (collectibleType)
            {
                case CollectibleType.Health:
                    if (collision.gameObject.GetComponent<PlayerController>().AddHealth(collectibleValue))
                        Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
