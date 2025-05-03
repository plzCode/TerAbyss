using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController player => GetComponentInParent<PlayerController>();

    public void Idle_state()
    {
        player.stateMachine.ChangeState(player.playerIdleState);
    }

}
