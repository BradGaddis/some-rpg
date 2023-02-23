using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS
public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationHandler playerAnimation;
    // Collider2D[] attackHitboxes;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] CircleCollider2D attackCollider;
    bool canAttack = true;
    

    protected void Start() {
        playerAnimation = GetComponent<PlayerAnimationHandler>();
        // TEMPORARY
        // attackCollider = attackHitboxes[Array.FindIndex(attackHitboxes,gameo => gameo.gameObject.name == "Forward Punch") ];
        // attackObject.SetActive(false);
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    virtual protected void Update() {
        StartAttack();
    }

    virtual public void StartAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            attackCollider.enabled = true;
            // playerAnimation.StartAttack();
            StartCoroutine(Attack());
        }
    }

    virtual protected IEnumerator Attack() {
        yield return new WaitForSeconds(attackDuration);
        attackCollider.enabled = false;
        canAttack = true;
    }

    // virtual protected  void GetAttackHitboxes() {
    //     attackHitboxes = GetComponentsInChildren<Collider2D>();
    // }

    virtual public void DealDamage(float damage, Enemy enemy) {
        Health enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.TakeDamage(damage);
    }


    virtual protected void OnTriggerEnter2D(Collider2D other) {
        Component[] components = other.gameObject.GetComponents(typeof(Component));
        Enemy enemy = null;

        foreach (Component component in components) {
            if (component as Enemy) {
                enemy = component as Enemy;
                DealDamage(attackDamage, enemy);
                break;
            }
        }
    }
}
