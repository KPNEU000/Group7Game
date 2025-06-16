using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int startingHealth = 10;
    public GameObject player;
    private int currentHealth;
    private int damage = 0;
    private Vector3 previousPosition;
    private float fallDistance;
    private float lastFallDistance;
    private bool isGrounded;

    public LevelManager levelManager;
    void Start()
    {
        isGrounded = true;

        currentHealth = startingHealth;

        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void FixedUpdate()
    {
        //Debug.Log("Player is grounded: " + isGrounded);

        // So long as the player is grounded, previousPosition will keep updating
        if (isGrounded)
        {
            previousPosition = player.transform.position;

        }
        // If not grounded, previousPosition minus the last point before
        // the player was grounded again will be the fall distance
        else
        {
            fallDistance = previousPosition.y - player.transform.position.y;
        }

        if (lastFallDistance > 0)
            TakeDamage(CalculateDamage(lastFallDistance));
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            lastFallDistance = fallDistance;

        }
        else if (collision.gameObject.CompareTag("Deadly")) //Die when you hit the water
        {
            Die();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        // Debug.Log("falling");
    }

    // Calculates amount of damage to take. For now it is only fall damage
    int CalculateDamage(float distanceFallen)
    {
        Debug.Log("fell " + distanceFallen);

        if (distanceFallen >= 6)
        {
            // each 4 height is 1 fall damage
            damage = (int)(distanceFallen / 6.0f);

            lastFallDistance = 0;
            return damage;

        }
        else
        {
            return 0;
        }
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateHealthSlider();

        Debug.Log(damage + " damage taken.");
        if (currentHealth <= 0)
        {
            // player dies
            Die();

        }
    }

    void Die()
    {
        Debug.Log("Player died.");

        transform.Rotate(-90, 0, 0, Space.Self);
        levelManager.LevelLost();
    }

    void UpdateHealthSlider()
    {
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }
    }
}
