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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
