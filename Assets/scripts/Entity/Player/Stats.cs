// create stats scriptable object
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    public float health;
    public int damage;
    public float speed;
    public float jumpForce;

    // get stats
    public float GetHealth() {
        return health;
    }

    public int GetDamage() {
        return damage;
    }

    public float GetSpeed() {
        return speed;
    }

    public float GetJumpForce() {
        return jumpForce;
    }

    // set stats
    public void SetHealth(int health) {
        this.health = health;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void SetJumpForce(float jumpForce) {
        this.jumpForce = jumpForce;
    }

    // add stats
    public void AddHealth(int health) {
        this.health += health;
    }


    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("Player Health: " + health);
    }

    public void DealDamage(int damage) {
        health -= damage;
        Debug.Log("Enemy Health: " + health);
    }

    public void AddPowerUp(PowerUp powerUp) {
        // health += powerUp.health;
        // damage += powerUp.damage;
        // speed += powerUp.speed;
        // jumpForce += powerUp.jumpForce;
    }
}