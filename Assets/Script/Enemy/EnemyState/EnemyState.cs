using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemy.animator.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        enemy.animator.SetBool(animBoolName, false);
    }
    public virtual void HandleInput() { }
    public virtual void Update() { }
}