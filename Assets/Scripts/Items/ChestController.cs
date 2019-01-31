using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] Loots;
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
        if(collision.collider.tag == "Weapon")
        {
            Debug.Log("Loots length -> " + Loots.Length);
            if (Loots.Length == 0)
                return;

            Destroy(gameObject);
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                int res = Random.Range(0, Loots.Length);
                Debug.Log("loot selected : " + res);
                GameObject loot = Instantiate(Loots[res]) as GameObject;
                loot.transform.position = transform.position;
            }
            
        }
    }
}
