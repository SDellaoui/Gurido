using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChestController : NetworkBehaviour
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
        if (collision.collider.tag == "Projectile")
        {
            CmdDestroyChest(collision.collider.gameObject.GetComponent<ProjectileController>().GetOwner());
        }
    }

    [Command]
    void CmdDestroyChest(GameObject player)
    {
        Debug.Log("Loots length -> " + Loots.Length);
        if (Loots.Length == 0)
            return;

        //GameNetworkManager.Instance.DestroyItem(gameObject);
        //Random.InitState((int)System.DateTime.Now.Millisecond);

        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            currentLootIndex = Random.Range(0, Loots.Length);
            GameObject loot = Instantiate(Loots[currentLootIndex]) as GameObject;
            loot.transform.position = transform.position;
            loot.GetComponent<CollectibleSpawnCurveController>().InitCurve(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            NetworkServer.Spawn(loot);
        }
        
        NetworkServer.Destroy(gameObject);
    }
}
