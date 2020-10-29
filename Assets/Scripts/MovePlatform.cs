using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public int speed = 5;
    public int limit = 5;
    public Vector3 lowerPos;
    public Vector3 higherPos;
    public bool change;
    public bool moveUp;
    public bool moveSide;
    public Vector3 left;
    public Vector3 right;

    // Start is called before the first frame update
    void Start()
    {
        lowerPos = transform.position - new Vector3(0, limit, 0);
        higherPos = transform.position + new Vector3(0, limit, 0);
        left = transform.position - new Vector3(limit, 0, 0);
        right = transform.position + new Vector3(limit, 0, 0);
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp == true)
        {
            MoveVertical();

            if (transform.position.y >= higherPos.y)
            {
                change = true;
            }

            if (transform.position.y <= lowerPos.y)
            {
                change = false;
            }
        }

        if (moveSide == true)
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
        }
    }

    public void MoveVertical()
    {
        if (change == false)
        {
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }

        if (change == true)
        {
            transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
    }

    public void MoveHorizontal()
    {
        if (change == false)
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }

        if (change == true)
        {
            transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
    }
}
