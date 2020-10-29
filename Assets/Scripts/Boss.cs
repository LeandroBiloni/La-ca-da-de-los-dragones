using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int jumpCounter = 0;
    public int jumpForce = 300;
    public Rigidbody2D RB;
    public int jumpRandom;
    public int jumpTime;
    public int jumpRandomLenght = 100;
    public int longJumpRandom;
    public int longJumpTime;
    public int longJumpRandomLenght = 300;
    public bool longJumpReset = false;
    public int attackRandom;
    public int attackTime;
    public int attackRandomLenght = 500;
    public int iceRandom;
    public int iceAttackTime;
    public int iceRandomLenght = 400;
    public bool isFlying;
    public int flyRandom;
    public int flyTime;
    public int flyRandomLenght = 200;
    public float flyDuration = 0;
    public int hitPoints;
    public Player player;
    public EnemyProjectile prefabProjectile;
    public IceAttack prefabIce;
    public Text showHP;
    public SpriteRenderer spRenderer;
    public FireBreath firePrefab;
    public int fireTime;
    public int fireRandomTime;
    public int fireRandomLenght = 800;
    public LightningAttack lightningPrefab;
    public int lightTime;
    public int lightRandomTime;
    public int lightRandomLenght = 400;
    public Collider2D collide;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        isFlying = false;
    }

    // Update is called once per frame
    void Update()
    {
        showHP.text = "Vida enemigo: " + hitPoints;

        if (isFlying == false)
        {
            Jump();

            if (longJumpReset == false)
            {
                LongJump();
            }
        }
        
        Attack();

        FireBreath();

        if (jumpCounter == 1)
        {
            Fly();
        }
        if (isFlying == true)
        {
            flyDuration += Time.deltaTime;
        }
        if (flyDuration >= 3)
        {
            RB.gravityScale = 1;
            flyDuration = 0;
            isFlying = false;
        }

        if (player.transform.position.x < gameObject.transform.position.x)
        {
            spRenderer.flipX = false;
        }
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            spRenderer.flipX = true;
        }

    }

    //SALTO ALEATORIO DEL ENEMIGO
    void Jump()
    {
        jumpRandom = Random.Range(0, jumpRandomLenght);
        jumpTime = Random.Range(0, jumpRandomLenght);

        if (jumpCounter < 2)
        {
            if (jumpRandom == jumpTime)
            {
                if (jumpCounter == 1)
                {
                    RB.AddForce(new Vector2(0, 1) * (jumpForce + 100));
                }

                RB.AddForce(new Vector2(0, 1) * jumpForce);
                jumpCounter++;
            }
                
        }



    }

    private void LongJump()
    {
        longJumpRandom = Random.Range(0, longJumpRandomLenght);
        longJumpTime = Random.Range(0, longJumpRandomLenght);
        if (longJumpTime == longJumpRandom && longJumpReset == false)
        {
            if (spRenderer.flipX == false)
            {
                RB.AddForce(new Vector2(-1, 1) * jumpForce*2);
                jumpCounter = 1;
                longJumpReset = true;
            }
            else
            {
                RB.AddForce(new Vector2(1, 1) * jumpForce*2);
                jumpCounter = 1;
                longJumpReset = true;
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision) // momento en el que toco un collider
    {

        //RESET SALTO
        if (collision.gameObject.layer == 8)
        {
            jumpCounter = 0;
            longJumpReset = false;
            collide.isTrigger = false;
        }

        if (collision.gameObject.layer == 14)
        {
            hitPoints -= player.projectileDmg;
            
            if (hitPoints <= 0)
            {
                showHP.text = "";
                GoToWin();  
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 18 || collision.gameObject.layer == 20)
        {
            collide.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collide.isTrigger = false;
    }


    void Attack()
    {
        attackRandom = Random.Range(0, attackRandomLenght);
        attackTime = Random.Range(0, attackRandomLenght);
        iceRandom = Random.Range(0, iceRandomLenght);
        iceAttackTime = Random.Range(0, iceRandomLenght);
        lightRandomTime = Random.Range(0, lightRandomLenght);
        lightTime = Random.Range(0, lightRandomLenght);

        if (attackTime == attackRandom)
        {
            if (spRenderer.flipX == false)
            {
                EnemyProjectile projectile = Instantiate(prefabProjectile);
                projectile.transform.position = transform.position - new Vector3(10, 0, 0);
            }
            else
            {
                EnemyProjectile projectile = Instantiate(prefabProjectile);
                projectile.transform.position = transform.position + new Vector3(10, 0, 0);
            }
                
        }

        else if (iceRandom == iceAttackTime)
        {
            if (spRenderer.flipX == false)
            {
                IceAttack projectile = Instantiate(prefabIce);
                projectile.transform.position = transform.position - new Vector3(10, 0, 0);
            }
            else
            {
                IceAttack projectile = Instantiate(prefabIce);
                projectile.transform.position = transform.position + new Vector3(10, 0, 0);
            }
        }

        else if (lightRandomTime == lightTime)
        {
            if (spRenderer.flipX == false)
            {
                LightningAttack projectile = Instantiate(lightningPrefab);
                projectile.transform.position = transform.position - new Vector3(10, 0, 0);
            }
            else
            {
                LightningAttack projectile = Instantiate(lightningPrefab);
                projectile.transform.position = transform.position + new Vector3(10, 0, 0);
            }
        }

    }

    public void Fly()
    {
        flyRandom = Random.Range(0, flyRandomLenght);
        flyTime = Random.Range(0, flyRandomLenght);

        if  (flyRandom == flyTime)
        {
            Jump();
            RB.gravityScale = 0;
            isFlying = true;
        }
    }

    public void FireBreath()
    {
        fireRandomTime = Random.Range(0, fireRandomLenght);
        fireTime = Random.Range(0, fireRandomLenght);

        if (fireRandomTime == fireTime)
        {
            transform.position = player.transform.position + new Vector3(0, 20, 0);
            FireBreath fire = Instantiate(firePrefab);
            fire.transform.position = transform.position - new Vector3(0, 5, 0);
        }
    }

    public void GoToWin()
    {
        SceneManager.LoadScene("Win");
    }
}
