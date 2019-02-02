using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] Loots;

    private int currentLootIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Weapon")
        {

            Debug.Log("Loots length -> " + Loots.Length);
            if (Loots.Length == 0)
                return;

            Destroy(gameObject);
            //Random.InitState((int)System.DateTime.Now.Millisecond);
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                currentLootIndex = Random.Range(0, Loots.Length);
                GameObject loot = Instantiate(Loots[currentLootIndex]) as GameObject;
                loot.transform.position = transform.position;
            }

        }
    }
}
