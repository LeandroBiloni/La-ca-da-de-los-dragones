using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Propiedades Personaje
    public int life = 5;
    public int maxLife;
    public float speed = 6;
    public float origSpeed;
    public float slowSpeed = 2.5f;
    public SpriteRenderer spRenderer;
    public Rigidbody2D RB;
    public Text showLife;   
    public Text showCollectibles;
    public bool canMove; //controla si puedo moverme o no
    public float stunTime = 3;
    public float slowTime = 3;
    public bool isSlowed; // indica si estoy ralentizado

    //COLECCIONABLES
    public int collectibleCounter = 0;
    public int extraLifeTrigger = 0;

    //Salto 
    public int JumpForce=300;
    public int origJumpForce;
    public int slowJumpForce = 150;
    int jumpCounter = 0;
    public float wallJumpTimer = 0f;
    float wallJumpMax = 2f;

    //OTROS
    public Vector3 spawnPoint;
    public Projectile slash;
    public Projectile magicSlash;
    public Projectile dragonSlash;
    public float currentTime = 0f;
    public int shootCounter = 0;
    public float shootDelay = 1f;
    public float origShootDelay;
    public float slowShootDelay = 2f;
    public int projectileDmg = 1;
    public Image atkOn;
    public Image atkOff;


    void Awake()
    {
        maxLife = life;
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position; //GUARDO POSICION DE INICIO
        canMove = true;
        isSlowed = false;
        origSpeed = speed;
        origShootDelay = shootDelay;
        origJumpForce = JumpForce;        
    }

    // Update is called once per frame
    void Update()
    {
        showLife.text = "Vidas: " + life;

        showCollectibles.text = "Poder magico: " + collectibleCounter;

        if (canMove == true)
        {
            Movement();
        }

        //DURACION STUN
        if (canMove == false)
        {
            stunTime -= Time.deltaTime;
        }

        if (stunTime <= 0)
        {
            canMove = true;
            stunTime = 3;
        }

        //DURACION SLOW
        if (isSlowed == true)
        {
            slowTime -= Time.deltaTime;
            speed = slowSpeed;
            JumpForce = slowJumpForce;
            shootDelay = slowShootDelay;
        }

        if (slowTime <= 0)
        {
            isSlowed = false;
        }

        if (isSlowed == false)
        {
            slowTime = 3;
            speed = origSpeed;
            JumpForce = origJumpForce;
            shootDelay = origShootDelay; 
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && canMove == true)
        {
            Jump();
        }

        //ICONO ATAQUE
        if (shootCounter == 0)
        {
            atkOn.gameObject.SetActive(true);
            atkOff.gameObject.SetActive(false);
        }
        else
        {
            atkOn.gameObject.SetActive(false);
            atkOff.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0) && shootCounter == 0 && canMove == true)
        {
            Slash();
            MagicSlash();
            DragonSlash();
        }

        //TIEMPO BALA
        if (shootCounter == 1)
        {
            currentTime += Time.deltaTime;
        }

        //RECARGA
        if (currentTime > shootDelay)
        {
            shootCounter = 0;
            currentTime = 0f;
        }

        if (extraLifeTrigger == 0)
        {
            if (collectibleCounter % 5 == 0 && collectibleCounter != 0)
            {
                life++;
                extraLifeTrigger = 1;
            }
        }
        if (collectibleCounter % 5 != 0)
        {
            extraLifeTrigger = 0;
        }
    }

    void DetectKey()
    {

 
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;

            //GIRA SPRITE PLAYER
            if (Input.GetAxis("Horizontal") > 0)

                spRenderer.flipX = false;
            else
                spRenderer.flipX = true;
        }
    }

    void Jump()
    {
        if (jumpCounter < 2)
        {
            if (jumpCounter == 1)
            {
                RB.AddForce(new Vector2(0, 1) * (JumpForce-200));
            }

            RB.AddForce(new Vector2(0, 1) * JumpForce);
            jumpCounter++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // momento en el que toco un collider
    {

        //RESET SALTO
        if (collision.gameObject.layer == 8)
        {
            jumpCounter = 0;
            RB.velocity = Vector2.zero;
        }

        if (collision.gameObject.layer == 10)
        {
            wallJumpTimer = 0f;
        }

        //RESPAWN - LIFE COUNTER
        if (collision.gameObject.layer == 4 || collision.gameObject.layer == 18 || collision.gameObject.layer == 17)
        {
            life--;
            canMove = true;
            isSlowed = false;
            if (life > 0)
            {
                transform.position = spawnPoint;
            }
            if (life == 0)
            {
                GoToLose();
                Destroy(gameObject);
            }
        }

        //STUN
        if (collision.gameObject.layer == 15)
        {
            canMove = false;
        }

        //SLOW
        if (collision.gameObject.layer == 20)
        {
            isSlowed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //WALL JUMP
        if (collision.gameObject.layer == 10)
        {
            wallJumpTimer += Time.deltaTime;
            if (wallJumpTimer <= wallJumpMax && Input.GetKeyDown(KeyCode.Space))
            {
                if (spRenderer.flipX == false)
                {
                    RB.AddForce(new Vector2(-1, 0) * (JumpForce + 150));
                    jumpCounter = 1;
                    spRenderer.flipX = true;
                    wallJumpTimer = 0;
                }
                else
                {
                    RB.AddForce(new Vector2(1, 0) * (JumpForce + 150));
                    jumpCounter = 1;
                    spRenderer.flipX = false;
                    wallJumpTimer = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //RESPAWN EN CHECKPOINT
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 16)
        {
            spawnPoint = transform.position;
        }

        if (collision.gameObject.layer == 12)
        {
            collectibleCounter++;
        }
    }

    void Slash()
    {
        if (collectibleCounter < 10)
        {
            if (spRenderer.flipX == false)
            {
                Projectile slashA = Instantiate(slash);
                slashA.transform.position = transform.position + new Vector3(2, 0, 0);
                shootCounter = 1;
                projectileDmg = 1;
            }
            else
            {
                Projectile slashA = Instantiate(slash);
                slashA.transform.position = transform.position - new Vector3(2, 0, 0);
                slashA.spRenderer.flipX = true;
                shootCounter = 1;
                projectileDmg = 1;
            }
        }

    }

    void MagicSlash()
    {
        if (collectibleCounter >= 10 && collectibleCounter < 20)
        {
            if (spRenderer.flipX == false)
            {
                Projectile slashB = Instantiate(magicSlash);
                slashB.transform.position = transform.position + new Vector3(2, 0, 0);
                shootCounter = 1;
                projectileDmg = 2;
            }
            else
            {
                Projectile slashB = Instantiate(magicSlash);
                slashB.transform.position = transform.position - new Vector3(2, 0, 0);
                slashB.spRenderer.flipX = true;
                shootCounter = 1;
                projectileDmg = 2;
            }
        }
    }

    void DragonSlash()
    {
        if (collectibleCounter >= 20)
        {
            if (spRenderer.flipX == false)
            {
                Projectile slashC = Instantiate(dragonSlash);
                slashC.transform.position = transform.position + new Vector3(2, 0, 0);
                shootCounter = 1;
                projectileDmg = 3;
            }
            else
            {
                Projectile slashC = Instantiate(dragonSlash);
                slashC.transform.position = transform.position - new Vector3(2, 0, 0);
                slashC.spRenderer.flipX = true;
                shootCounter = 1;
                projectileDmg = 3;
            }
        }
    }

    public void GoToLose()
    {
        SceneManager.LoadScene("Lose");
    }

}



