using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //public Camera thirdPersonCamera;
    public GameObject firstPersonCamera;
    public void UpdateCameraPosition(GameObject player, GameObject NPC, bool isExiting)
    {
        transform.position = Vector3.Lerp(player.transform.position, NPC.transform.position, 0.5f);
        firstPersonCamera.SetActive(isExiting);
    }
}
