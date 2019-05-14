using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    public int lifeValue;

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
        if (collision.name == "Player" && !collected)
        {
            collected = true;
            FindObjectOfType<AudioManager>().Play("LifeUpSound");
            Destroy(gameObject);
            playerInfo.LifeUp(lifeValue);
        }
    }
}
