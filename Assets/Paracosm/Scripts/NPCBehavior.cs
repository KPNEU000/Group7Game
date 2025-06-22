using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;


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
    [SerializeField]
    bool dialogueAvailable = false;
    public GameObject dialogueCanvas;
    public GameObject gameUI;
    public GameObject dialogueCamera;
    public TMP_Text dialogueText;
    public GameObject dialogueUnavailableText;
    public GameObject convincedNPCText;
    public GameObject unconvincedNPCText;
    public PlayerMovement pm;
    public static int requiredConvincing = 0;
    public static int convincedNPCs = 0;
    public TextMeshProUGUI endGameText;
    public bool isConvinced = true;
    public GameObject panel;
    [Header("FSM")]
    public NPCState currentState = NPCState.Idle;
    public bool walkable = false;
    public float detectionRange = 20f;
    public NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player GameObject
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null && SceneManager.GetActiveScene().name != "Level 2")
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene!");
        }


        if (!cameraAnchor)
        {
                cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor");
        }

        thirdPersonCameraAnchor = cameraAnchor.GetComponent<ThirdPersonCamera>();
        
        if (walkable)
        {
            agent.SetDestination(target.position);
            if (!agent)
            {
                walkable = false;
            }
        }
        else
        {
            if (agent)
            {
                agent.enabled = false;
            }
        }
    }

    void Update()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (!pm)
        {
            pm = target.GetComponent<PlayerMovement>();
        }
        switch (currentState)
        {
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
        Debug.Log(requiredConvincing + "/" + convincedNPCs);
        //End Game
        if (requiredConvincing == convincedNPCs && requiredConvincing != 0)
        {
            StartCoroutine("TemporaryText", endGameText);
            endGameText.text = convincedNPCs.ToString() + "/8 NPCs were convinced";
            Invoke("EndGame", 5);
        }
        
    }

    void EndGame()
    {
        Debug.Log("GAME ENDED");
        Application.Quit();
    }

    void Idle() {
        if (text3) {
            text3.SetActive(false);
        }
        if (walkable && agent.enabled) {
            agent.enabled = false;
        }

        LookForPlayer();
    }

    void Notice() {
        if (text3) {
            text3.SetActive(true);
        }
        if (walkable && !agent.enabled) {
            agent.enabled = true;
        }
        if (walkable && HasLineOfSight()) {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }

        if (Vector3.Distance(transform.position, target.position) > detectionRange) {
            currentState = NPCState.Idle;
        }
    }

    void Talk() {
        if (text3) {
            text3.SetActive(false);
        }
        if (walkable && agent.enabled) {
            agent.enabled = false;
        }

        pm = target.GetComponent<PlayerMovement>();
        //Check if you have the right clue to enter dialogue
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.T))
            {

                currentState = NPCState.Talk;
                if (dialogueEnabled)
                {
                    foreach (GameObject c in pm.GetClues())
                    {
                        if (c == null)
                        {
                            dialogueAvailable = false;
                        }
                        else if (c == clue)
                        {
                            dialogueAvailable = true;
                            if (isConvinced)
                            {
                                requiredConvincing++;
                                isConvinced = false;
                            }
                        }
                    }
                    if (dialogueAvailable)
                    {
                        UnityEngine.Cursor.visible = true;
                        UnityEngine.Cursor.lockState = CursorLockMode.None;
                        dialogueCamera.SetActive(true);
                        dialogueText.gameObject.SetActive(true);
                        dialogueCanvas.gameObject.SetActive(true);
                        gameUI.SetActive(false);
                    }
                    else
                    {
                        StartCoroutine("TemporaryText", dialogueUnavailableText);
                    }
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

    IEnumerator TemporaryText(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }

    public void CorrectDialogue()
    {
        if (dialogueEnabled)
        {
            convincedNPCs++;
            StartCoroutine("TemporaryText", convincedNPCText);
            StartCoroutine("TemporaryText", panel);
            dialogueEnabled = false;
        }
    }

    public void IncorrectDialogue()
    {
        if (dialogueEnabled)
        {
            StartCoroutine("TemporaryText", unconvincedNPCText);
            StartCoroutine("TemporaryText", panel);
            requiredConvincing--;
            if (requiredConvincing == convincedNPCs && convincedNPCs == 0 && requiredConvincing == 0)
            {
                StartCoroutine("TemporaryText", endGameText);
                endGameText.text = convincedNPCs.ToString() + "/8 NPCs were convinced";
                Invoke("EndGame", 5);
            }
            dialogueEnabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentState = NPCState.Idle;
        text1.SetActive(false);
        text2.SetActive(false);

        thirdPersonCameraAnchor.UpdateCameraPosition(target.gameObject, gameObject, true);
        if (dialogueCamera)
        {
            dialogueCamera.SetActive(false);
            dialogueCanvas.SetActive(false);
            gameUI.SetActive(true);
            //UnityEngine.Cursor.visible = false;
            //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    void LookForPlayer() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        Transform player = null;

        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Player")) {
                player = collider.transform;
            }
        }
        
        if (player && HasLineOfSight()) {
            currentState = NPCState.Notice;
        }
    }

    bool HasLineOfSight() {
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange)) {
            if (hit.collider.CompareTag("Player")) {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}

