using UnityEngine;
using UnityEngine.Networking;

public enum ItemType {Chest };
public class ItemNetWorkSpawner : NetworkBehaviour
{
    public ItemType itemType;
    // Start is called before the first frame update
    void Start()
    {
        switch (itemType)
        {
            case ItemType.Chest:
                GameObject item = Instantiate(Resources.Load("Prefabs/Items/Chest"),transform.position,Quaternion.identity) as GameObject;
                NetworkServer.Spawn(item);
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
