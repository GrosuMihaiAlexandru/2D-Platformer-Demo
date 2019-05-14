using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage;

    public PlayerController2D player;
    public PlayerInfo playerInfo;

    private float cooldown;

    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && cooldown <= 0)
        {
            cooldown = player.maxInvincibilityPeriod;
            playerInfo.Damage(1, player.invincible);
            player.damage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            player.damage = false;
        }
    }
}
