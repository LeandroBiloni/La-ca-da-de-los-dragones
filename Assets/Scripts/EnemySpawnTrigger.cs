using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject enemy;
    public Player player;
    public Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {

            enemy.SetActive(true);
            if (player.collectibleCounter < 10)
            {
                boss.hitPoints = 20;
            }

            if (player.collectibleCounter >= 10 && player.collectibleCounter < 20)
            {
                boss.hitPoints = 50;
            }

            if (player.collectibleCounter >= 20)
            {
                boss.hitPoints = 100;
            }

            Debug.Log("Spawneo el enemigo");
        }
    }
}
