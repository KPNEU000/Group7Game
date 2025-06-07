using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //public Camera thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject player;

    void Update()
    {
        //transform.position = player.transform.position;
    }
    public void UpdateCameraPosition(GameObject player, GameObject NPC, bool isExiting)
    {
        if (!isExiting)
        {
            transform.position = (player.transform.position + NPC.transform.position)/2 ;
        }
        else
        {
            transform.position = player.transform.position;
        }
        firstPersonCamera.SetActive(isExiting);
    }
}
