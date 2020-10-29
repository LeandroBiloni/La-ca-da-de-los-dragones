using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed=5;
    float maxTime;
    public float currentTime;
    public SpriteRenderer spRenderer;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            Destroy(gameObject);
        }

    }
    private void move()
    {
        if (spRenderer.flipX == false)
        {
            transform.position += new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
        }
        else transform.position -= new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 18 || collision.gameObject.layer == 17 || collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }
}
