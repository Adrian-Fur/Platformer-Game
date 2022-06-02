using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxEnemyHealth = 100;
    int currentEnemyHealth;
    Animator animator;

    void Awake() 
    {
        animator= GetComponent<Animator>();
    }
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
