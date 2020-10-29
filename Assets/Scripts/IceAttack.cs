using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public float speed = 10;
    public GameObject target;
    Vector3 director;
    public bool gotDirection = false;
    public SpriteRenderer sprite;
    public Collider2D collide;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        if (target.transform.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gotDirection == false)
        {
            Direction();
        }
        Move();
    }


    void Direction()
    {
        director = target.transform.position - transform.position;
        gotDirection = true;
    }

    void Move()
    {
        transform.position += director.normalized * speed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 13 || collision.gameObject.layer == 11 || collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 17 || collision.gameObject.layer == 15 || collision.gameObject.layer == 20)
        {
            collide.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collide.isTrigger = false;
    }
}
