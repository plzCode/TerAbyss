using UnityEngine;

public class EnemyDieState : EnemyState
{
    public EnemyDieState(Enemy enemy, EnemyStateMachine stateMachine, string _animBoolName) : base(enemy, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true; // Set the enemy to busy state

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
    }

    public override void Update()
    {
        base.Update();
    }
}
