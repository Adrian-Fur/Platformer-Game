using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerControl playerControl;
    Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int maxPlayerHealth = 100;
    int currentPlayerHealth;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
    }

    void Start() 
    {
        currentPlayerHealth = maxPlayerHealth;
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
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
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
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

    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentPlayerHealth <= 0)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject);
    }
}
