using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State 
    {
        Walking,
        ChaseTarget
    }

    public int maxEnemyHealth = 100;
    int currentEnemyHealth;
    [SerializeField] float enemyMoveSpeed = 1f;
    float inputHorizontal;
    bool facingLeft = true;

    Animator animator;
    Rigidbody2D rigidBody;
    PlayerControl player;
    Vector3 enemyPosition;
    State state;

    void Awake() 
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerControl>();
        state = State.Walking;
    }
    void Start()
    {
        enemyPosition = transform.position;
        currentEnemyHealth = maxEnemyHealth;
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(-enemyMoveSpeed, 0f);
        
    }

    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        animator.SetTrigger("Hurt");
        animator.SetBool("isWalking", false);

        if (currentEnemyHealth <= 0)
        {
            EnemyDie();
        }
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

    void EnemyDie()
    {
        animator.SetBool("isDead", true);
        enemyMoveSpeed = 0f;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void FindTarget()
    {
        float targetRange = 50f;
        
        if (Vector3.Distance(transform.position, player.PlayerPosition()) < targetRange)
        {

        }
    }
    
}
