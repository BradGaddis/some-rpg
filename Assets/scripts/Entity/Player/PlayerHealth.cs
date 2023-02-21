using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private void Awake() {
        isPlayer = true;
        isEnemy = false;
    }

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        if (health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Player died");
        }
    }
}
