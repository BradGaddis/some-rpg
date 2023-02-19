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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // override public void TakeDamage(int damage) {
    //     health -= damage;
    //     if (health <= 0) {
    //         Die();
    //     }
    //     else {
    //         Debug.Log("Player took " + damage + " damage");
    //     }
    // }

    override protected void Die() {
    }
}
