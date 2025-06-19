using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using NUnit.Framework;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;
    public bool canLoadClueList = true;

    [Header("Controls")]
    public float speed = 10f;
    public float jumpHeight = 0.5f;
    public float gravity = 9.81f;
    public float airControl = 10f;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;

    Animator animator;

    private AudioSource playerAudioSource;

    [Header("Audio")]
    public AudioClip genericWalkSFX;

    bool grounded;
    public float range = 1;

    [Header("Inventory")]
    public List<GameObject> keys;
    public List<GameObject> clues = new List<GameObject>();
    public int cluesCollected = 0;
    public List<String> clueStrings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        UpdatePlayerAnim(0);
        InvokeRepeating("PlayWalkSound", 0, 0.2f);
    }

    // Update is called once per frame
    /*
    void FixedUpdate()
    {
        //RaycastingEffect();
        //Debug.Log(grounded);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Debug.Log(controller.isGrounded);
        //if (animator.GetInteger("animState") != 1)
        //{
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            UpdatePlayerAnim(4);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            UpdatePlayerAnim(3);
        }
        if (moveHorizontal == 0
        && moveVertical == 0
        && controller.isGrounded)
        {
            UpdatePlayerAnim(0);
        }
        moveDirection.y -= gravity * Time.deltaTime; //Apply gravity constantly
        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();


        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
            //UpdatePlayerAnim(2);
            //grounded = false;
               moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
            moveDirection.y = 0.0f; //reset
            }
        }
        else //midair
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            //UpdatePlayerAnim(5); //The isGrounded variable keeps flickering, so before that's fixed I'll just comment out these animations
        }



        controller.Move(input * speed * Time.deltaTime);


        //}
    }
    */

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Level 3"
        && canLoadClueList)
        {
            LoadClueList();
            canLoadClueList = false;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();

        //Debug.Log(controller.isGrounded);
        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f; //reset
            }
            //Debug.Log(moveDirection.y);
        }
        //else //midair
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime; //Apply gravity constantly
        if (moveHorizontal != 0 || moveVertical > 0)
        {
            UpdatePlayerAnim(4);
        }
        if (moveVertical < 0)
        {
            UpdatePlayerAnim(3);
        }
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            UpdatePlayerAnim(0);
        }
        controller.Move(input * speed * Time.deltaTime);
    }

    public void PlayWalkSound()
    {
        if (controller.isGrounded && input != Vector3.zero)
        {
            playerAudioSource.pitch = UnityEngine.Random.Range(0, 5);
            playerAudioSource.PlayOneShot(genericWalkSFX);
        }
    }


    public void UpdatePlayerAnim(int animState)
    {
        animator.SetInteger("animState", animState);
    }

    public void AddClueToList(GameObject clue) //Add the string of the picked up clue to this list so it can be reloaded at the start of level 3
    {
        clueStrings.Add(clue.name);
    }

    public void LoadClueList()
    {
        foreach (String c in clueStrings)
        {
            clues.Add(GameObject.Find(c));
            //Debug.Log("ATTEMPTED TO LOAD LIST");
        }
    }

    public List<GameObject> GetClues()
    {
        return clues;
    }
/*
                    void OnCollisionEnter(Collision collision)
                    {
                        Debug.Log("Collided with " + collision.transform.name);

                        //GROUNDED CHECK
                        ContactPoint ground = collision.contacts[0]; //The first point of collision, which in this case is the floor 
                        if(ground.normal.y > 0.5f) //roughly horizontal surface
                        {
                            isGrounded = true;
                        }
                        Debug.Log("Contact position: " + ground.point);
                        Debug.Log("Contact normal: " + ground.normal); //1 if horizontal, 0 if vertical 
                    }
                    */

}
