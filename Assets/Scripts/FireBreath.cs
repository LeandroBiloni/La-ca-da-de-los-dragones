using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    float speed=5;
    public SpriteRenderer spRenderer;
    public Collider2D collide;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
            transform.position -= new Vector3(0f, 1f, 0f) * speed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 17)
        {
            collide.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collide.isTrigger = false;
    }
}
