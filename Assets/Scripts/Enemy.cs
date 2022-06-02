using UnityEngine;

public class Enemy : MonoBehaviour
{
  
    public int maxEnemyHealth = 100;
    int currentEnemyHealth;
    [SerializeField] float enemyMoveSpeed = 1f;
    bool facingLeft = true;
    [SerializeField] float chaseDistance = 5f;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    bool canAttack = false;

    Animator animator;
    Rigidbody2D rigidBody;
    public Transform attackPoint;
    PlayerCombat playerCombat;
    GameObject player;
  
    void Awake() 
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerCombat = FindObjectOfType<PlayerCombat>();
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    void Update() 
    {
        rigidBody.velocity = new Vector2(-enemyMoveSpeed, 0f);
        if (player == null)
        {
            canAttack = false;
        }
         if (Time.time >= nextAttackTime)
        {
            if (IsRangeToPlayer() && playerCombat != null && canAttack)
            {
                AttackPlayer();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            
        }
    }

    void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerCombat.TakeDamage(attackDamage);
        }
    }

    public bool IsRangeToPlayer()
    {   
        canAttack = true;
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentEnemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        facingLeft = !facingLeft;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
   
    
}
