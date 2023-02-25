using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] Stats playerStats;

    private void Awake() {
        isPlayer = true;
        isEnemy = false;
    }

    void Update()
    {
        playerStats.health = Mathf.Clamp(playerStats.health, 0, maxHealth);
    }

    override public void ReduceHealth(float damage) {
        playerStats.health -= damage;
        if (currentHealth <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ResetFullHealth();
            Debug.Log("Player died");
        }
    }

    override public float GetHealth() {
        currentHealth = playerStats.health;
        return currentHealth;
    }

    private void SetHealth(float health) {
        this.currentHealth = playerStats.health;
    }

    public void ResetFullHealth() {
        playerStats.health = maxHealth;
    }
}
