using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS
// The player controller should actually handle the attack input

public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationHandler playerAnimation;
    // Collider2D[] attackHitboxes;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float timeTilImpact = 0.5f;
    private CircleCollider2D attackCollider;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool wasHit = false;

    Vector3 inputMovement;
    Vector3 attackRange;  
    float attackAOE;
    Vector3 prevPos;

    protected void Start() {
        playerAnimation = GetComponentInParent<PlayerAnimationHandler>();
        // TEMPORARY
        // attackCollider = attackHitboxes[Array.FindIndex(attackHitboxes,gameo => gameo.gameObject.name == "Forward Punch") ];
        // attackObject.SetActive(false);
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        // attackCollider.enabled = false;
        timeTilImpact = timeTilImpact > 0 ? timeTilImpact : 0;
        // attack range is the length of the attack collider's origin from zero
        attackRange = new Vector3(attackCollider.bounds.center.x, attackCollider.bounds.center.y, 0) - Vector3.zero;
        attackAOE = attackCollider.radius;
        Debug.Log(attackRange);
    }

    virtual protected void Update() {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // use V key to attack
        ChangePosition();
        InitiateAttack();
    }

    void ChangePosition() {
            if (inputMovement != Vector3.zero && canAttack) {
                attackCollider.offset = inputMovement;
            } 
    }

    virtual public void InitiateAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            // isAttacking = true;

            // Purely for animation purposes
            playerAnimation.StartForwardPunchAttack();
            
            StartCoroutine(AttackDuration());
        }
    }

    virtual protected IEnumerator AttackDuration() {
        Vector3 startMove = inputMovement;
        if(timeTilImpact > 0)
        {
            yield return new WaitForSeconds(timeTilImpact);
        }
        // This block is the actual duration of the attack 
        isAttacking = true;
        // This method handles the actual attack
        AttackEnemy();
        yield return new WaitForSeconds(attackDuration);
        // This is the end of the attack
        canAttack = true;
        isAttacking = false;
        wasHit = false;
        // if (startMove != inputMovement) {
        //     prevPos = inputMovement;
        //     // if we moved while attacking, we should update the collider offset
        //     Vector3 newPos = inputMovement - startMove;
        //     newPos = new Vector3(newPos.x / newPos.x, newPos.y / newPos.y, 0);
        //     attackCollider.offset = newPos;
        // }
    }

    // virtual protected  void GetAttackHitboxes() {
    //     attackHitboxes = GetComponentsInChildren<Collider2D>();
    // }

    virtual public void DealDamage(float damage, Enemy enemy) {
        Health enemyHealth = enemy.GetComponent<Health>();
        if(enemyHealth != null) enemyHealth.TakeDamage(damage);
    }

    virtual protected void AttackEnemy(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        // find the enemy by components 
        foreach (Collider2D collider in colliders) {
            Component[] components = collider.gameObject.GetComponents(typeof(Component));
            Enemy enemy = null;
            foreach (Component component in components) {
                if (component as Enemy && isAttacking && !wasHit){
                    enemy = component as Enemy;
                    wasHit = true;          
                    DealDamage(attackDamage, enemy);
                    break;
                }
            }
        }

    }    

}
