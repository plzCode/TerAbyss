using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
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

public class PlayerSitState : PlayerState
{
    public PlayerSitState(PlayerController player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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
public class PlayerAttackState1 : PlayerState
{
    public PlayerAttackState1(PlayerController player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.attackEffect1.SetActive(true);
        player.isBusy = true;
    }
    public override void Exit()
    {
        base.Exit();
        player.attackEffect1.SetActive(false);
        player.isBusy = false;
    }
    public override void HandleInput()
    {
    }
    public override void Update()
    {
        base.Update();
    }
}