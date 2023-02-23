using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS
public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationHandler playerAnimation;
    Collider2D[] attackHitboxes;
    GameObject attackObject;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    bool canAttack = true;
    
    private void Start() {
        playerAnimation = GetComponent<PlayerAnimationHandler>();
        // GetAttackHitboxes();
        // TEMPORARY
        // attackCollider = attackHitboxes[Array.FindIndex(attackHitboxes,gameo => gameo.gameObject.name == "Forward Punch") ];
        attackObject = GameObject.Find("Forward Punch");
    }

    private void Update() {
        StartAttack();
    }

    public void StartAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            attackObject.SetActive(true);
            playerAnimation.StartAttack();
            StartCoroutine(Attack());
            DealDamage(attackDamage);
        }
    }

    private IEnumerator Attack() {
        yield return new WaitForSeconds(attackDuration);
        attackObject.SetActive(false);
        canAttack = true;
    }

    private void GetAttackHitboxes() {
        attackHitboxes = GetComponentsInChildren<Collider2D>();
    }

    public float DealDamage(float damage) {
        return damage;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        TryGetComponent<Enemy>(out Enemy enemy);
        if (enemy != null || enemy is Enemy) {
            enemy.TakeDamage(DealDamage(attackDamage));
            Debug.Log("Enemy hit for " + attackDamage + " damage, enemy health is now " + enemy.GetHealth() );
        } else 
        {
            Debug.Log("Enemy is null");
        }

    }
}
