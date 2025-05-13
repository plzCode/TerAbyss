using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    #region States
    public EnemyStateMachine stateMachine;
    public EnemyIdleState enemyIdleState;
    public EnemyDieState enemyDieState;
    public EnemyAttackState enemyAttackState;
    #endregion

    [Header("References")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Settings")]
    public float detectionRange = 10f; // Range to detect the player
    public float attackRange = 2f;     // Range to attack the player
    public float attackDamage = 10f;   // Damage dealt to the player

    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        enemyIdleState = new EnemyIdleState(this, stateMachine, "Idle");
        enemyDieState = new EnemyDieState(this, stateMachine, "Die");
        enemyAttackState = new EnemyAttackState(this, stateMachine, "Attack");

        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(enemyIdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Handle enemy-specific damage logic here
        // For example, play a hit animation or sound effect
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        // Handle enemy-specific death logic here
        // For example, play a death animation or drop loot
        stateMachine.ChangeState(enemyDieState);
    }
}
