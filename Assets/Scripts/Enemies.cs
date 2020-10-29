using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int jumpCounter = 0;
    public int jumpForce = 300;
    public Rigidbody2D RB;
    public SpriteRenderer spRenderer;
    public int currentTime;
    public int jumpTime;
    public int attackTime;
    public int jumpRandom = 100;
    public int attackRandom = 300;
    public int hitPoints;
    public Player player;
    public EnemyProjectile prefabProjectile;


    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 5;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    //SALTO ALEATORIO DEL ENEMIGO
    void Jump()
    {
        currentTime = Random.Range(0, jumpRandom);
        jumpTime = Random.Range(0, jumpRandom);

        if (jumpCounter < 1)
        {
            if (currentTime == jumpTime)
            {
                RB.AddForce(new Vector2(0, 1) * jumpForce);
                jumpCounter++;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) // momento en el que toco un collider
    {

        //RESET SALTO
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            jumpCounter = 0;
        }

        if (collision.gameObject.layer == 14)
        {
            hitPoints -= player.projectileDmg;

            if (hitPoints <= 0)
            {
                player.collectibleCounter++;
                Destroy(gameObject);
            }
        }

    }

    void Attack()
    {
        currentTime = Random.Range(0, attackRandom);
        attackTime = Random.Range(0, attackRandom);

        if (attackTime == currentTime)
        {
            if (spRenderer.flipX == false)
            {
                EnemyProjectile projectile = Instantiate(prefabProjectile);
                projectile.transform.position = transform.position - new Vector3(4, 0, 0);
            }

            else
            {
                EnemyProjectile projectile = Instantiate(prefabProjectile);
                projectile.transform.position = transform.position + new Vector3(4, 0, 0);
            }
                
        }
                
    }
}
