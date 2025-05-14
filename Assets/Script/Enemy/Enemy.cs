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
    public float speed = 0f;
    public bool isBusy = false; // Flag to check if the enemy is busy

    private float lastAttackTime;

    //for navigation
    private float detectionCooldown = 0.2f;
    private float lastCheckTime;


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        enemyIdleState = new EnemyIdleState(this, stateMachine, "Idle");
        enemyDieState = new EnemyDieState(this, stateMachine, "Die");
        enemyAttackState = new EnemyAttackState(this, stateMachine, "Attack_1");

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
        DetectAndMove();

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

    public void DetectAndMove()
    {
        if (player == null || isBusy) return;
                
        //Check and move to player in Range
        if (Time.time - lastCheckTime > detectionCooldown)
        {
            lastCheckTime = Time.time;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime > 1f)
            {
                // Attack the player
                Enemy_Attack();
            }

            else if (distanceToPlayer <= detectionRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                // Speed 값으로 애니메이션 전환
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            else
            {
                // 멈추고 Idle 애니메이션으로 전환
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);
            }
        }
        
        
    }
    public void Enemy_Attack()
    {
        lastAttackTime = Time.time;
        stateMachine.ChangeState(enemyAttackState);
    }

    public void SetMoveSpeed(float speed)
    {
        this.speed = speed;
        animator.SetFloat("Speed", speed);
    }    
        
}
