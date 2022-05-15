using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : Mirror.NetworkBehaviour
{

    [SerializeField]
    public Behaviour[] componentsToDisable;

    Camera sceneCamera;

    void Start ()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour playerBehaviour in componentsToDisable)
            {
                playerBehaviour.enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
         if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

}
