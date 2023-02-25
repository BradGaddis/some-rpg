using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected float maxHealth = 100;

    [SerializeField] protected int healthModifier = 1;    
    [SerializeField] protected bool isPlayer = false;
    [SerializeField] protected bool isEnemy = true;

    virtual public void ReduceHealth(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Debug.Log(this.gameObject.name + " died");
            Die();
        }
        else {
            Debug.Log(this.gameObject.name + " took " + damage + " damage");
        }
    }
    
    virtual public float GetHealth() {
        return currentHealth;
    }

    virtual protected void Die(){
            Destroy(this.gameObject);
    }
}
