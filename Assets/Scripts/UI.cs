using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Variabila de Testare
    public int maxLevelScore;

    public Text lives;
    public Text score;

    public Texture fullHealth;
    public Texture emptyHealth;

    public RawImage[] health;

    private PlayerInfo player;

    void Start()
    {
        player = FindObjectOfType<PlayerInfo>();
    }

    void Update()
    {
        lives.text = player.Lives.ToString();
        score.text = player.Score.ToString() + "/" + maxLevelScore;

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < health.Length; i++)
            if (i < player.Health)
                health[i].texture = fullHealth;
            else
                health[i].texture = emptyHealth;
    }

}
