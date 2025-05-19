using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().hasKey)
            {
                gameObject.SetActive(false);
                other.GetComponent<PlayerController>().hasKey = false;
            }
        }
    }
}
