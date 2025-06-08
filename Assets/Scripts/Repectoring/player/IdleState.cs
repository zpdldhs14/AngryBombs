using UnityEngine;

public class IdleState : IPlayerState
{
    public void EnterState(playerMovement player)
    {
        player.animator.SetFloat("Speed",0);
    }

    public void UpdateState(playerMovement player)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            player.ChangeState(new MoveState());
        }
    }

    public void ExitState(playerMovement player)
    {
        
    }
}