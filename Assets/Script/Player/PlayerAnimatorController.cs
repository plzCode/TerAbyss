using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController player => GetComponentInParent<PlayerController>();

    public void Idle_state()
    {
        player.stateMachine.ChangeState(player.playerIdleState);
    }

    public void UseSkill01()
    {
        if(player.skillStates[0] != null)
        {
            player.skillStates[0].UseSKill();
        }
    }
    public void UseSkill02()
    {
        if (player.skillStates[1] != null)
        {
            player.skillStates[1].UseSKill();
        }
    }

}
