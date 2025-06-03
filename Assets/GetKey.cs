using UnityEngine;

public class GetKey : MonoBehaviour
{

    public float range = 10;

    [SerializeField]
    PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastingEffect();
    }
    
     void RaycastingEffect() 
    {
        RaycastHit objectHitByRaycast;
        if (Physics.Raycast(transform.position, transform.forward, out objectHitByRaycast, range))
        {
            if (objectHitByRaycast.collider.CompareTag("Key"))
            {
                //objectHitByRaycast.transform.GetComponent<KeyBehavior>().Glow();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objectHitByRaycast.transform.GetComponent<KeyBehavior>().PickedUp();
                    playerMovement.UpdatePlayerAnim(1);
                    playerMovement.keys.Add(gameObject);
                }
            }
        }
    }
}
