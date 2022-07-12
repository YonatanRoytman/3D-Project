using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{
    //Enemy hp bar
    public int maxHealth;
    public EnemyHealthBar healthBar;
    private int curentHealth;

    Animator anm;
    public float lookRadius = 10f;

    //find the position of player
    Transform target;
    NavMeshAgent agent;

    //attack timer
    public float attackRate = 3.5f;
    private float attackRateTimeStamp = 0f;

    void Start()
    {
        //Enemy max hp
        curentHealth = maxHealth;

        // Find the player gameobject
        target = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        //Animation
        anm = GetComponent<Animator>();
    }

    void Update()
    {
        //Distance is the distance between the player.position, and the enemy.position
        float distance = Vector3.Distance(target.position, transform.position);
        anm.SetBool("move_forward", false);
        anm.SetBool("idle_normal", true);

        //if the distance smaller than lookRadius(10) the enemy will start to follow the player.position
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anm.SetBool("move_forward", true);
            anm.SetBool("idle_normal", false);


            // if the distance is smaller than stoppingDistance and the timing is correct then the enemy will atack
            if (distance <= agent.stoppingDistance && Time.time > attackRateTimeStamp)
            {
                // Attack the player
                anm.SetBool("move_forward", false);
                anm.SetBool("idle_normal", false);
                anm.SetTrigger("Attack");

                //looking at the player so he cant get behind
                transform.LookAt(target.transform.position);
                //reset the timer
                attackRateTimeStamp = Time.time + attackRate;
            }
            //if the timing is wrong the enemy just idle 
            else if (distance <= agent.stoppingDistance)
            {
                anm.SetBool("move_forward", false);
                anm.SetBool("idle_normal", true);
                //looking at the player so he cant get behind
                transform.LookAt(target.transform.position);
            }
        }

        if (curentHealth == 0)
        {
            anm.SetTrigger("Dead");
            anm.SetBool("move_forward", false);
            anm.SetBool("idle_normal", false);
            Destroy(gameObject, 8);

            agent.speed = 0;

        }
    }

    public void TakeDamage(int damage)
    {
        curentHealth -= damage;
        healthBar.UpdateHealth((float)curentHealth / (float)maxHealth);
    }


    // Draw a red radius (not important just for visual) 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (collided.tag == "Sword")
        {
            anm.SetTrigger("Damage");
            anm.SetBool("move_forward", false);
            anm.SetBool("idle_normal", false);
            TakeDamage(1);

        }
        if (collided.tag == "MagicBall")
        {
            anm.SetTrigger("Damage");
            anm.SetBool("move_forward", false);
            anm.SetBool("idle_normal", false);
            TakeDamage(3);
            Destroy(collided);

        }
    }
}
