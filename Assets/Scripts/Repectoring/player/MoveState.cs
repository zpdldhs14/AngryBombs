using UnityEngine;

public class MoveState : IPlayerState
{
    public void EnterState(playerMovement player)
    {
        
    }

    public void UpdateState(playerMovement player)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        player.controller.Move(move * player.speed * Time.deltaTime);
        player.animator.SetFloat("Speed", move.magnitude);

        if (move.magnitude < 0.1f)
        {
            player.ChangeState(new IdleState());
        }
    }

    public void ExitState(playerMovement player)
    {
        
    }
}