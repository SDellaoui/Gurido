using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkBehaviour
{
    public static GameNetworkManager Instance = null;
    PlayerNetworkSetup playerNetworkComponent;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayer(GameObject player)
    {
        playerNetworkComponent = player.GetComponent<PlayerNetworkSetup>();
    }

    // Network commands

    public void DestroyItem(GameObject item)
    {
        playerNetworkComponent.CmdDestroyItem(item);
    }
}
