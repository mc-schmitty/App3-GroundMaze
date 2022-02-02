using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Rigidbody rb;
    Animator anim;
    public float damage = 80;
    bool canMove;
    bool isAttacking;
    bool canHit;
    public AudioClip startScream;
    public AudioClip attack;
    public AudioClip deathScream;
    public AudioClip move;
    bool screamS;

    AudioSource audioPlayer;
    AudioSource spiderShuffle;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        AudioSource[] aList = GetComponents<AudioSource>();
        audioPlayer = aList[0];
        spiderShuffle = aList[1];
        canMove = true;
        isAttacking = false;
        canHit = false;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.isDead)
        {
            if (canMove)
            {
                // move
                nav.SetDestination(player.position);
                anim.SetBool("IsMove", true);

            }
            else if (isAttacking)
            {
                // try attack
                anim.SetBool("IsMove", false);
                anim.SetBool("IsAttack", true);
                spiderShuffle.Stop();
            }
        }
        else { 
            //idle
            anim.SetBool("IsMove", false);
            spiderShuffle.Stop();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
            isAttacking = true;
        
            if (canHit)
            {
                // Hit player
                Debug.Log("Hit!");
                playerHealth.TakeDamage(damage);
                EndAttack();
            }
        }
    }

    public void StartAttack()
    {
        canHit = true;
        audioPlayer.pitch = Random.Range(0.95f, 1.05f);
        audioPlayer.clip = attack;
        audioPlayer.Play();
        //Debug.Log("Attack Started");
    }

    public void EndAttack()
    {
        canHit = false;
        isAttacking = false;
        canMove = true;
        anim.SetBool("IsAttack", isAttacking);
        anim.SetBool("IsMove", canMove);
        spiderShuffle.Play();
        //Debug.Log("Attack Ended");
    }

    public void Die()
    {
        spiderShuffle.Stop();
        // Turn spider into a ball
        audioPlayer.clip = deathScream;
        audioPlayer.Play();
        
        anim.SetTrigger("Die");
        canHit = false;
        nav.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        this.enabled = false;
    }
}
