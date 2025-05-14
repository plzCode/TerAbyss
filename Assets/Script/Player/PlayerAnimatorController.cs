using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController player => GetComponentInParent<PlayerController>();

    public void Idle_state()
    {
        player.stateMachine.ChangeState(player.playerIdleState);
    }


    public void UseSkill00()
    {
        if (player.skillStates[0] != null)
        {
            //player.stateMachine.ChangeState(player.skillStates[0]);
            player.skillStates[0].UseSKill();
        }

    }
    public void UseSkill01()
    {
        if(player.skillStates[1] != null)
        {
            //player.stateMachine.ChangeState(player.skillStates[1]);
            player.skillStates[1].UseSKill();
        }
    }
    public void UseSkill02()
    {
        if (player.skillStates[2] != null)
        {
            //player.stateMachine.ChangeState(player.skillStates[2]);
            player.skillStates[2].UseSKill();
        }
    }

}
