using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Enemy enemy => GetComponent<Enemy>();

    public void Idle_state()
    {
        enemy.stateMachine.ChangeState(enemy.enemyIdleState);
    }
}
