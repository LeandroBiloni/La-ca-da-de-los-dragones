using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy : MonoBehaviour
{
    public Player player;
    public int speed = 4;
    public int hitPoints;
    public SpriteRenderer spRenderer;

    public IceAttack prefabProjectile;
    public int currentTime;
    public int attackTime;
    public int attackRandom;

    public int invisibleTime;
    public int invisibleRandom;
    public bool isInvisible;
    public float invisibleCounter = 0;
    public Collider2D collide;

    public Vector3 left;
    public Vector3 right;
    public int limit = 15;
    public bool change;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 10;
        invisibleRandom = 100;
        isInvisible = false;
        attackRandom = 150;
        left = transform.position - new Vector3(limit, 0, 0);
        right = transform.position + new Vector3(limit, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();

        if (transform.position.x >= right.x)
        {
            change = true;
        }

        if (transform.position.x <= left.x)
        {
            change = false;
        }

        Invisible();
        
        if (isInvisible == true)
        {
            invisibleCounter += Time.deltaTime;
        }

        if (invisibleCounter >= 3)
        {
            spRenderer.enabled = true;
            isInvisible = false;
            invisibleCounter = 0;
        }
        Attack();

        if (hitPoints <= 0)
        {
            player.collectibleCounter++;
            Destroy(gameObject);
        }
    }



    void Invisible()
    {
        currentTime = Random.Range(0, invisibleRandom);
        invisibleTime = Random.Range(0, invisibleRandom);

        if (currentTime == invisibleTime)
        {
            spRenderer.enabled = false;
            isInvisible = true;
        }
    }

    void Attack()
    {
        currentTime = Random.Range(0, attackRandom);
        attackTime = Random.Range(0, attackRandom);
        if (attackTime == currentTime & isInvisible == true)
        {
            
            if (spRenderer.flipX == false)
            {
                IceAttack projectile = Instantiate(prefabProjectile);
                projectile.transform.position = transform.position - new Vector3(4, 0, 0);
            }

            else
            {
                 IceAttack projectile = Instantiate(prefabProjectile);
                 projectile.transform.position = transform.position + new Vector3(4, 0, 0);
            }
        }
    }

    public void MoveHorizontal()
    {
        if (change == false)
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            spRenderer.flipX = true;
        }

        if (change == true)
        {
            transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
            spRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            hitPoints--;
        }


        //BALAS ATRAVIESAN EN VEZ DE ROMPERSE
        if (collision.gameObject.layer == 15)
        {
            collide.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collide.isTrigger = false;
    }
}
