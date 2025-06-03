using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    private bool open;
    [SerializeField]
    private Animator doorAnimator;

    public GameObject correctKey;
    public bool openable;

    [Header("Audio")]
    [SerializeField]
    private AudioClip doorLocked;
    [SerializeField]
    private AudioClip doorOpened;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (open)
        {
            doorAnimator.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject key in other.GetComponent<PlayerMovement>().keys)
            {
                if (key == correctKey)
                {
                    openable = true;
                }
            }
            if (openable)
                {
                    open = true;
                    //other.GetComponent<PlayerMovement>().key = null;
                    AudioSource.PlayClipAtPoint(doorOpened, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(doorLocked, transform.position);
                }
        }
    }

}
