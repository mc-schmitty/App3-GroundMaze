using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 150;
    public float currentHealth;
    public Image damageImage;
    public bool isDead;
    public AudioClip damageClip;    // Might not be necessary
    public AudioClip dieClip;
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    AudioSource playerAudio;
    PlayerMove playerMove;
    

    void Start()
    {
        currentHealth = startingHealth;
        playerMove = GetComponent<PlayerMove>();
        playerAudio = GetComponent<AudioSource>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageImage != null)
        {
            // Screen either matches red color for health, or fades to white when dead
            Color c = damageImage.color;
            if (!isDead)
            {
                c.a = 1f - currentHealth / startingHealth;
                currentHealth = Mathf.Min(currentHealth + 2 * Time.deltaTime, startingHealth);
            }
            else
            {
                c = Color.Lerp(c, Color.black, Time.deltaTime);
                if (damageImage.color == c)
                {
                    TransitionScene();
                }
            }
            
            damageImage.color = c;
            
        }
        else
        {
            // Whats the worst that can happen???
            damageImage = FindObjectOfType<Image>();
        }


    }

    public void TakeDamage(float damageAmount)
    {
        //currentHealth = Mathf.Max(currentHealth - damageAmount, 0);   // Cool code but unnecessary
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Death();
        }
        else
        {
            playerAudio.pitch = Random.Range(0.9f, 1.1f);
            playerAudio.Play();
        }
        

    }

    void Death()
    {
        isDead = true;
        playerAudio.clip = dieClip;
        playerAudio.Play();
        playerMove.enabled = false;
    }

    void TransitionScene()
    {
        // Destroy the player, then move to the next scene;
        GameObject.Destroy(gameObject);
        SceneManager.LoadScene("DeathScreen");
    }
}
