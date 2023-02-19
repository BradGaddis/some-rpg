using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int health = 100;
    [SerializeField]
    protected int healthModifier = 1;    
    [SerializeField]
    protected bool isPlayer = true;
    [SerializeField]
    protected bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Debug.Log(this.gameObject.name + " died");
        }
        else {
            Debug.Log(this.gameObject.name + " took " + damage + " damage");
        }
    }
}
