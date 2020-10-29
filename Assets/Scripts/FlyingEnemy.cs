using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public int speed = 4;
    public int hitPoints;
    public SpriteRenderer spRenderer;
    public Player player;
    public EnemyProjectile prefabProjectile;
    public int currentTime;
    public int attackTime;
    public int attackRandom;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 3;
        attackRandom = 300;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
       if (spRenderer.flipX == false)
        {
            transform.position += new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
        }
       else transform.position -= new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 19)
        {
            if (spRenderer.flipX == false)
            {
                spRenderer.flipX = true;
            }
            else spRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
}
