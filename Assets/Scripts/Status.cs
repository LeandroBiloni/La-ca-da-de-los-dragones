using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Player player;
    public SpriteRenderer stun;
    public SpriteRenderer ice;
    public Text showTimers;

    // Start is called before the first frame update
    void Start()
    {
        stun.enabled = false;
        ice.enabled = false;
        showTimers.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        IconPosition();
        
        if (player.canMove == false || player.isSlowed == true)
        {
            showTimers.enabled = true;
        }

        if (player.canMove == true && player.isSlowed == false)
        {
            showTimers.enabled = false;
        }

        if (player.canMove == false)
        {
            stun.enabled = true;
            showTimers.text = "Aturdido: " + (int) (player.stunTime +1);
        }

        if (player.canMove == true)
        {
            stun.enabled = false;
        }

        if (player.isSlowed == false)
        {
            ice.enabled = false;
        }

        if (player.isSlowed == true)
        {
            ice.enabled = true;
            showTimers.text = "Ralentizado: " + (int) (player.slowTime +1);
        }
    }

    void IconPosition()
    {
        stun.transform.position = player.transform.position + new Vector3(0, 3, 0);
        ice.transform.position = player.transform.position + new Vector3(0, 3, 0);
    }


}
