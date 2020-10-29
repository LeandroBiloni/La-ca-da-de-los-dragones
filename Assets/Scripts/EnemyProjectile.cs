using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject target;
    float speed = 4;
    public Collider2D collide;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
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
