using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collider might not work the way that I intended it. I used a trigger, but apparently you need some sort of movement to trigger the trigger. I'll have to look into this more.

//  THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS


public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationHandler playerAnimation;
    // Collider2D[] attackHitboxes;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    private CircleCollider2D attackCollider;
    bool canAttack = true;
    [SerializeField] float impactTime = 0.5f;
    private float impactTimer = 0f;

    

    protected void Start() {
        playerAnimation = GetComponentInParent<PlayerAnimationHandler>();
        // TEMPORARY
        // attackCollider = attackHitboxes[Array.FindIndex(attackHitboxes,gameo => gameo.gameObject.name == "Forward Punch") ];
        // attackObject.SetActive(false);
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
        impactTimer = impactTime;
    }

    virtual protected void Update() {
        StartAttack();
    }

    virtual public void StartAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            playerAnimation.StartForwardPunchAttack();
            StartCoroutine(Attack());
        }
    }

    virtual protected IEnumerator Attack() {
        yield return new WaitForSeconds(impactTime);
        attackCollider.enabled = true;
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
            if (component  as Enemy) {
                enemy = component as Enemy;
                DealDamage(attackDamage, enemy);
                break;
            }
        }
    }

    virtual protected void OntriggerStay2D(Collider2D other) {
        Component[] components = other.gameObject.GetComponents(typeof(Component));
        Enemy enemy = null;

        foreach (Component component in components) {
            if (component as Enemy && canAttack) {
                enemy = component as Enemy;
                DealDamage(attackDamage, enemy);
                break;
            }
        }
    }

    private IEnumerator ImpactTimer() {
        yield return new WaitForSeconds(impactTime);

    }
}
