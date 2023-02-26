using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS

// This class defaults to a forward punch attack for testing purposes, but can be used for other attacks as well
public class PlayerAttack : MonoBehaviour
{
    // Collider2D[] attackHitboxes;
    [SerializeField] protected float attackDuration = 2f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected float timeTilImpact = 0.5f;
    [SerializeField] protected float particleTimeOut = 0.5f;
    protected bool canAttack = true;
    protected bool isAttacking = false;
    protected bool enemyWasHit = false;
    private Vector3 moveDir;
    private float attackRange;  
    private float attackAOE;
    private Vector3 prevPos;

    // Componenet References
    [SerializeField] ParticleSystem attackParticles;
    private CircleCollider2D attackCollider;
    private PlayerAnimationHandler playerAnimation;
    private PlayerInput playerInput;

    protected void Start() {
        playerAnimation = GetComponentInParent<PlayerAnimationHandler>();
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        playerInput = GetComponentInParent<PlayerInput>();
        timeTilImpact = timeTilImpact > 0 ? timeTilImpact : 0; // time should always be positive
        attackRange = attackCollider.offset.magnitude;
        attackAOE = attackCollider.radius;
        Debug.Log("Attack range: " + attackRange);
    }

    virtual protected void Update() {
        moveDir = playerInput.GetMoveDirection();
        // use V key to attack
        ChangePosition(); 
        InitiateAttack();
    }

    /// <summary>
    /// This method is used to change the position of the attack collider
    /// </summary>
    virtual protected void ChangePosition() {
            if (playerInput.IsMoving() && canAttack) {
                attackCollider.offset = moveDir;
            } 
    }

    /// <summary>
    /// This method is used to initiate the attack process
    /// </summary>
    virtual protected void InitiateAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            // isAttacking = true;

            // Purely for animation purposes
            playerAnimation.StartForwardPunchAttack();

            StartCoroutine(AttackDuration());
        }
    }

    /// <summary>
    /// This method is used to handle logic of the duration of the attack
    /// </summary>
    virtual protected IEnumerator AttackDuration() {
        Vector3 startPos = transform.parent.position;

        if(timeTilImpact > 0)
        {
            yield return new WaitForSeconds(timeTilImpact);
        }
        // This block is the actual duration of the attack 
        isAttacking = true;
        // This method handles the actual attack
        AttackEnemy();
        yield return new WaitForSeconds(playerAnimation.GetAnimationClipLength());
        canAttack = true;
        isAttacking = false;
        enemyWasHit = false;
        
        
        Vector3 currentPosition = transform.parent.position;
        if (startPos != currentPosition) {
            // if we moved while attacking, we should update the collider offset
            Vector3 newPos = currentPosition - startPos;
            // constrain new position length to attack range
            if (newPos.magnitude != attackRange) {
                newPos = newPos.normalized * attackRange;
            } 
            attackCollider.offset = newPos;
        }

    }
    
    /// <summary>
    /// This method is used to handle the actual attack. Such as dealing damage to the enemy dealing knockback to the enemy
    /// </summary>
    virtual protected void AttackEnemy() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        // find the enemy by components 
        foreach (Collider2D collider in colliders) {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable) && isAttacking && !enemyWasHit){
                ParticleSystem particles = Instantiate(attackParticles, attackCollider.bounds.center, Quaternion.identity);
                Destroy(particles.gameObject, particleTimeOut);                
                enemyWasHit = true;       
                damageable.TakeDamage(attackDamage);
                break;
            }
        }
    }

}
