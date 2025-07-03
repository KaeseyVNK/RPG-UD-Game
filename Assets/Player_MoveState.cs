using UnityEngine;

public class Player_MoveState : Player_GroundState
{
    public Player_MoveState(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {

    }


    public override void Update()
    {
        base.Update();
        if (player.movementInput.x == 0 || player.wallDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetVelocity(player.movementInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
