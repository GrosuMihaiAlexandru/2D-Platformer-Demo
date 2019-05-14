using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private bool alive;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int lives;
    [SerializeField] private int score;

    #region Properties
    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    #endregion


    public void LifeUp(int amount)
    {
        lives += amount;
    }

    // Resets the score to 0
    public void ScoreReset()
    {
        score = 0;
    }

    // Increases the total score of the player
    public void ScoreUp(int amount)
    {
        score += amount;
    }

    // Sets the heath of the player to maxHealth
    public void FullHealth()
    {
        Health = MaxHealth;
    }

    // Increases the heath of the player by the given amount
    public void HealthUp(int amount)
    {
        if (Health < MaxHealth)
            Health += amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    // Decreases the health of the player by the given amount
    public void Damage(int amount, bool invincible)
    {
        if (!invincible)
        {
            Health -= amount;
            if (Health <= 0)
                Death();
        }
    }

    // Kills the player
    public void Death()
    {
        Alive = false;
        lives--;
    }
}
