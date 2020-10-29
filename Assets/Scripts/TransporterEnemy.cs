using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterEnemy : MonoBehaviour
{
    public int hitPoints;
    public SpriteRenderer spRenderer;
    public GameObject player;
    public Collider2D collide;
    Vector3 playerPos;
    public Player playerStats;

    public int maxRandom, minRandom;
    public int randomX, randomY;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 5;
        maxRandom = 10;
        minRandom = -10;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();

        Teleport();

        timer += Time.deltaTime;

        if (timer >= 3)
        {
            timer = 0;
        }

        if (hitPoints <= 0)
        {
            playerStats.collectibleCounter++;
            Destroy(gameObject);
        }
    }
    
    void FindPlayer() // obtiene posicion del jugador
    {
        playerPos = player.transform.position;
    }

    void Teleport()
    {
        if (timer == 0)
        {
            randomX = Random.Range(minRandom, maxRandom);
            randomY = Random.Range(minRandom, maxRandom);
            transform.position = playerPos + new Vector3(randomX, randomY, 0); //se transporta en base a la posicion del jugador

            if (player.transform.position.x < transform.position.x)
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
            hitPoints--;
        }

        //PLAYER/ENEMIGO ATRAVIESAN EN VEZ DE ROMPERSE
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 15)
        {
            collide.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collide.isTrigger = false;
    }
}
