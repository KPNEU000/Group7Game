using UnityEngine;
using TMPro;

public class GetKey : MonoBehaviour //Should really be renamed GetCollectable or CameraRaycasting
{

    public float range = 10;

    [SerializeField]
    PlayerMovement playerMovement;

    [Header("HUD")]
    public TMP_Text inspect;
    public TMP_Text keyInventory;
    public TMP_Text clueInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastingEffect();
        //Gizmos.DrawLine(transform.position + Vector3.forward, transform.position + Vector3.forward * range);
    }

    void RaycastingEffect()
    {
        RaycastHit objectHitByRaycast;
        if (Physics.Raycast(transform.position, transform.forward, out objectHitByRaycast, range))
        {
            if (objectHitByRaycast.collider.CompareTag("NPC") || objectHitByRaycast.collider.CompareTag("Door") || objectHitByRaycast.collider.CompareTag("Key") || objectHitByRaycast.collider.CompareTag("Clue"))
            {
                inspect.text = objectHitByRaycast.transform.name;
            }
            else
            {
                //inspect.text = objectHitByRaycast.transform.name;
                inspect.text = "";
            }

            if (objectHitByRaycast.collider.CompareTag("Key"))
            {
                //objectHitByRaycast.transform.GetComponent<KeyBehavior>().Glow();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objectHitByRaycast.transform.GetComponent<KeyBehavior>().PickedUp();
                    playerMovement.UpdatePlayerAnim(1);
                    playerMovement.keys.Add(objectHitByRaycast.transform.gameObject);
                    keyInventory.text = keyInventory.text + "\n" + objectHitByRaycast.transform.name;
                }
            }
            if (objectHitByRaycast.collider.CompareTag("Clue"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objectHitByRaycast.transform.GetComponent<ClueBehavior>().PickedUp();
                    playerMovement.UpdatePlayerAnim(1);
                    PlayerMovement.clues.Add(objectHitByRaycast.transform.gameObject);
                    PlayerMovement.cluesCollected++;
                    clueInventory.text = clueInventory.text + "\n" + objectHitByRaycast.transform.name;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
}
