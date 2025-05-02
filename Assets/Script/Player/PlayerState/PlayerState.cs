using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player; 
    private string animBoolName;

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter() 
    {
        player.animator.SetBool(animBoolName, true);
    }
    public virtual void Exit() 
    {
        player.animator.SetBool(animBoolName, false);
    }
    public virtual void HandleInput() { }
    public virtual void Update() { }
}