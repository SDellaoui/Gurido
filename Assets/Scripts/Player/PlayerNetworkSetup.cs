using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    private void Start()
    {
        DisableComponents();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CmdAddDamage(10);
        }
    }
    private void DisableComponents()
    {
        if (!isLocalPlayer)
        {
            //Désactiver les components des autres joueurs sur notre instance
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.transform.gameObject.SetActive(false);
        }
    }
    
    private void OnDisable()
    {
        sceneCamera.transform.gameObject.SetActive(true);
    }

    [Command]
    public void CmdAddDamage(int dmg)
    {
        gameObject.GetComponent<PlayerController>().AddDamage(dmg);
    }

    [Command]
    public void CmdAddHealth(int health)
    {
        gameObject.GetComponent<PlayerController>().AddHealth(health);
    }

    [Command]
    public void CmdDestroyItem(GameObject item)
    {
        NetworkServer.Destroy(item);
    }
}
