using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerControl playerControl;
    Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (playerControl.isRunning == true)
        {
            animator.SetTrigger("RunningAttack");
        }
        else
        {
            animator.SetTrigger("Attack");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
        
    }

    void OnDrawGizmosSelected() 
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
