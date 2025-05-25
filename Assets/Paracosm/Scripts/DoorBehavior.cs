using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip doorLocked;
    [SerializeField]
    private AudioClip doorOpened;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().hasKey)
            {
                gameObject.SetActive(false);
                other.GetComponent<PlayerController>().hasKey = false;
                AudioSource.PlayClipAtPoint(doorOpened, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(doorLocked, transform.position);
            }
        }
    }
}
