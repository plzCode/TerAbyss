using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    public void Idle_state()
    {
        enemy.stateMachine.ChangeState(enemy.enemyIdleState);
    }
}
