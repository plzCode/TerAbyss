using System.Collections;
using UnityEngine;

public class PlayerSkillState : PlayerState
{
    private Skill skillToUse; 

    public PlayerSkillState(PlayerController player, PlayerStateMachine stateMachine, string _animBoolName, Skill skill)
        : base(player, stateMachine, _animBoolName)
    {
        this.skillToUse = skill;
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;

    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
    }

    public void UseSKill()
    {
        if (skillToUse.CanActivate())
        {
            skillToUse.Use(player);
        }
    }
    
}