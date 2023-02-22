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

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        if (currentHealth <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Player died");
        }
    }

    override public float GetHealth() {
        return 0;
    }

    private void SetHealth(float health) {
        this.currentHealth = playerStats.health;
    }

    private void ResetFullHealth() {
        currentHealth = maxHealth;
    }
}
