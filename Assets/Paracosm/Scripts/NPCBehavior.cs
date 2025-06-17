using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public Transform target;
    public GameObject cameraAnchor;
    public GameObject clue;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public Transform head;

    public float maximumX;
    public float minimumX;

    public float maximumY;
    public float minimumY;

    public float maximumZ;
    public float minimumZ;

    public Quaternion minQuaternion = new Quaternion(0.02620f, 0.91192f, -0.40526f, 0.05895f);
    public Quaternion maxQuaternion = new Quaternion(0.34818f, 0.21019f, -0.08042f, 0.91001f);

    public GameObject thirdPersonCamera;
    public ThirdPersonCamera thirdPersonCameraAnchor;

    public enum NPCState {Idle, Notice, Talk}

    [Header("Dialogue")]
    public bool dialogueEnabled;
    public GameObject dialogueCanvas;
    public GameObject gameUI;
    public GameObject dialogueCamera;
    public TMP_Text dialogueText;

    [Header("FSM")]
    public NPCState currentState = NPCState.Idle;
    public bool walkable = false;
    public NavMeshAgent agent;

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
        thirdPersonCameraAnchor = cameraAnchor.GetComponent<ThirdPersonCamera>();
        if (walkable) {
            agent.SetDestination(target.position);
            if (!agent) {
                walkable = false;
            }
        }
    }

    void Update() {
        switch (currentState) {
            case NPCState.Idle:
                Idle();
                break;
            case NPCState.Notice:
                Notice();
                break;
            case NPCState.Talk:
                Talk();
                break;
        }
    }

    void Idle() {
        if (text3) {
            text3.SetActive(false);
        }
        if (walkable && agent.enabled) {
            agent.enabled = false;
        }
    }

    void Notice() {
        if (text3) {
            text3.SetActive(true);
        }
        if (walkable && !agent.enabled) {
            agent.enabled = true;
        }
        if (walkable) {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }

    void Talk() {
        if (text3) {
            text3.SetActive(false);
        }
        if (walkable && agent.enabled) {
            agent.enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentState == NPCState.Idle) {
                currentState = NPCState.Notice;
            }

            if (Input.GetKey(KeyCode.T))
            {
                currentState = NPCState.Talk;
                if (dialogueEnabled)
                {
                    dialogueCamera.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    dialogueCanvas.gameObject.SetActive(true);
                    gameUI.SetActive(false);
                }
                else
                {
                    if (clue.GetComponent<ClueBehavior>().collected == true)
                    {
                        text2.SetActive(true);
                    }
                    else
                    {
                        text1.SetActive(true);
                    }

                    thirdPersonCameraAnchor.UpdateCameraPosition(target.gameObject, gameObject, false);
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentState = NPCState.Idle;
        text1.SetActive(false);
        text2.SetActive(false);

        thirdPersonCameraAnchor.UpdateCameraPosition(target.gameObject, gameObject, true);
        if (dialogueEnabled)
        {
            dialogueCamera.SetActive(false);
            dialogueCanvas.SetActive(false);
            gameUI.SetActive(false);
        }
    }

    void LateUpdate() //Called after everything in the Update field 
    {
        /*
        if (target && head)
        {
            Vector3 direction = target.position - head.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            //
            if (lookRotation != minQuaternion && lookRotation != maxQuaternion)
            {
                head.LookAt(target.position);
            }
            else
            {
                Debug.Log(gameObject.name + "ROTATION" + lookRotation);
            }
            /*
                head.transform.eulerAngles = new Vector3(
        Mathf.Clamp(head.transform.eulerAngles.x, minimumX, maximumX),
        Mathf.Clamp(head.transform.eulerAngles.y, minimumY, maximumY),
        Mathf.Clamp(head.transform.eulerAngles.z, minimumZ, maximumZ)
    );

                Vector3 direction = target.position - head.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                head.rotation = Quaternion.Slerp(head.rotation, lookRotation, 15 * Time.deltaTime);
                }
            */
        
        }
    }

