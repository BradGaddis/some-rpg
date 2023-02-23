using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITakeDamage
{
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected float maxHealth = 100;

    [SerializeField] protected int healthModifier = 1;    
    [SerializeField] protected bool isPlayer = true;
    [SerializeField] protected bool isEnemy = false;

    virtual public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Debug.Log(this.gameObject.name + " died");
        }
        else {
            Debug.Log(this.gameObject.name + " took " + damage + " damage");
        }
    }
    
    virtual public float GetHealth() {
        return currentHealth;
    }

    virtual protected void Die(){

    }
}
