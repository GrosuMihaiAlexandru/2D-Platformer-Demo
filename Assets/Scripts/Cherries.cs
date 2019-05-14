using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherries : MonoBehaviour
{
    public int cherryValue;

    private PlayerController2D player;
    private PlayerInfo playerInfo;

    private bool collected;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pickup if health < maxhealth
        if (collision.name == "Player" && playerInfo.Health < playerInfo.MaxHealth && !collected)
        {
            collected = true;
            FindObjectOfType<AudioManager>().Play("CherrySound");
            Destroy(gameObject);
            playerInfo.HealthUp(cherryValue);
        }
    }
}
