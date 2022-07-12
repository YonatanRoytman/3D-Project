using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerEngine : MonoBehaviour
{
    public GameObject cat1;
    public GameObject cat2;
    //Gift
    public GameObject gift;

    //DodgingTimer
    public float dogde = 1.5f;
    private float dogdeTimeStamp = 0f;
    //hp
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    //Elixer
    public int maxElixer = 8;
    public int currentElixer;
    public ElixerBar elixerBar;

    //speed
    public float speed;


    //Gravity
    public bool isGround;
    Rigidbody rb;
    float force;

    //NpcEngine
    private GameObject DialougeSystem;
    private bool trigerring;

    //Animation 
    Animator anm;

    //MagicAttack
    public GameObject Knife;
    public Transform shootingPoint;
    public float magicSpeed = 25;
    bool launched;

    private void Start()
    {
        // hp = 10 on start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //Elixer = 8 on Start
        currentElixer = maxElixer;
        elixerBar.SetMaxElixer(maxElixer);

        //Movement
        speed = 4f;

        //Gravity
        rb = GetComponent<Rigidbody>();
        force = 10;

        //Animation
        anm = GetComponent<Animator>();
    }

    void Update()
    {       
        PlayerMovement();
        PlayerAttack();
    }

    void PlayerMovement()
    {
        //Movement
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 PlayerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;

        ////Rotate the Player in Local space
        transform.Translate(PlayerMovement, Space.Self);

        //running anm
        if (Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.W))
        {
            anm.SetBool("IsRunning", true);
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            speed = 6f;
            transform.Translate(PlayerMovement, Space.Self);
        }
        //walking straight anm
       else if (Input.GetKey(KeyCode.W) && isGround == true)
        {
            anm.SetBool("IsWalking", true);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);
        }
        //walking right anm
        else if (Input.GetKey(KeyCode.D) && isGround == true)
        {
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", true);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);
        }
        //walking left anm
        else if(Input.GetKey(KeyCode.A) && isGround == true)
        {
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", true);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);

        }
        //walking back anm
        else if(Input.GetKey(KeyCode.S) && isGround == true)
        {
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", true);
            anm.SetBool("IsRunning", false);

        }
        // idle anm
        else
        {
            anm.SetBool("IsIdle", true);
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);
        }
        //dodging anm
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > dogdeTimeStamp && isGround == true)
        {
            anm.SetBool("IsDodging", true);
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);


            rb.AddForce(-transform.forward * 400, ForceMode.Force);
            dogdeTimeStamp = Time.time + dogde;
        }
        // jumping anm
        if (Input.GetKeyDown(KeyCode.Space) & isGround == true)
        {
            anm.SetTrigger("Jump");

            isGround = false;
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);

        }


    }

    // Attack anm
    private void PlayerAttack()
    {
      
        if (Input.GetMouseButtonDown(0) && isGround == true)
        {
            anm.SetTrigger("MeleeAttack");
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);

        }
        if (currentHealth <= 0)
        {
            anm.SetTrigger("Dead");
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);

            SceneManager.LoadScene(1);
            

        }

        if (Input.GetKeyDown(KeyCode.X)&&currentElixer>0)
        {
            launched = true;
            anm.SetTrigger("MagicAttack");
            anm.SetBool("IsIdle", false);
            anm.SetBool("IsWalking", false);
            anm.SetBool("IsDodging", false);
            anm.SetBool("IsWalkingRight", false);
            anm.SetBool("IsWalkingLeft", false);
            anm.SetBool("IsWalkingBack", false);
            anm.SetBool("IsRunning", false);

        }

       
    }
    public void IsLunchedTrue()
    {
        if (launched == true)
        {
            ThrowMagicBall();
            ElixerUse(1);

        }
    }
    public void ThrowMagicBall()
    {
        GameObject KnifeInstance = Instantiate(Knife, shootingPoint.position, Knife.transform.rotation);
        KnifeInstance.transform.rotation = Quaternion.LookRotation(-shootingPoint.forward);
        Rigidbody KnifeRig = KnifeInstance.GetComponent<Rigidbody>();

        KnifeRig.AddForce(shootingPoint.forward * magicSpeed, ForceMode.Impulse);
        launched = false;
    }
    
    public  void Jumping()
    {
        rb.AddForce(transform.up * force, ForceMode.Impulse);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Find the ground
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
      else  if (collision.collider.tag == "Trap")
        {
            rb.AddForce(transform.up * 600, ForceMode.Force);
            TakeDamage(5);
        }
      else  if (collision.collider.tag == "DamageEnemy")
        {
            rb.AddForce(-transform.forward * 450, ForceMode.Force);
            rb.useGravity = true;
            TakeDamage(2);
        }
        else if(collision.collider.tag == "BossDamage")
        {
            rb.AddForce(-transform.forward * 550, ForceMode.Force);
            rb.useGravity = true;
            TakeDamage(3);
        }
        
    }
    private void OnTriggerEnter(Collider collided)
    {
        if (collided.tag == "HealthPotion" && currentHealth < 10)
        {
            Heal();
            healthBar.SetHealth(currentHealth);
            Destroy(collided.gameObject);
        }
        if(collided.tag == "ElixarPotion" && currentElixer < 8)
        {
            currentElixer++;
            elixerBar.SetElixer(currentElixer);
            Destroy(collided.gameObject);
        }
        if(collided.tag == "Cat")
        {
            cat1.SetActive(false);
            cat2.SetActive(true);
            gift.SetActive(true);
        }
    }

    // Function that let you change the number of damage
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    void ElixerUse(int elixer)
    {
        currentElixer -= elixer;
        elixerBar.SetElixer(currentElixer);
    }
    void Heal()
    {
        currentHealth++;
        currentHealth++;
        currentHealth++;
        currentHealth++;
        currentHealth++;
    }

}
