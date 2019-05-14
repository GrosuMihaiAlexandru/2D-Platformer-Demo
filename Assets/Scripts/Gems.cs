using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int gemValue;

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
        if (collision.name == "Player" && !collected)
        {
            collected = true;
            FindObjectOfType<AudioManager>().Play("GemSound");
            Destroy(gameObject);

            playerInfo.ScoreUp(gemValue);
        }
    }
}
