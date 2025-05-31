using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform target;
    public GameObject cameraAnchor;
    public GameObject clue;
    public GameObject text1;
    public GameObject text2;
    public Transform head;

    public float maximumX;
    public float minimumX;

    public float maximumY;
    public float minimumY;

    public float maximumZ;
    public float minimumZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (!cameraAnchor)
        {
            cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (clue.GetComponent<ClueBehavior>().collected == true)
            {
                text2.SetActive(true);
            }
            else
            {
                text1.SetActive(true);
            }

            cameraAnchor.GetComponent<ThirdPersonCamera>().UpdateCameraPosition(target.gameObject, gameObject, false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        text1.SetActive(false);
        text2.SetActive(false);

        cameraAnchor.GetComponent<ThirdPersonCamera>().UpdateCameraPosition(target.gameObject, gameObject, true);
    }

    void LateUpdate() //Called after everything in the Update field 
    {
        if (target && head)
        {
            head.LookAt(target.position);
            head.transform.eulerAngles = new Vector3 (
    Mathf.Clamp(head.transform.eulerAngles.x, minimumX, maximumX),
    Mathf.Clamp(head.transform.eulerAngles.y, minimumY, maximumY),
    Mathf.Clamp(head.transform.eulerAngles.z, minimumZ, maximumZ)
);
        }
    }
}
