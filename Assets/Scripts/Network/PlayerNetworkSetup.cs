using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            //Désactiver les components des autres joueurs sur notre instance
            for(int i = 0;  i< componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
                Camera.main.transform.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        sceneCamera.transform.gameObject.SetActive(true);
    }
}
