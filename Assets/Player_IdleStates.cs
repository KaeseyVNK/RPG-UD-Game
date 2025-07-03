using UnityEngine;

public class Player_IdleStates : Player_GroundState
{
    public Player_IdleStates(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {

    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(player.movementInput.x == player.facingDirection && player.wallDetected){
            return;
        }

        if (player.movementInput.x != 0 )
        {
            stateMachine.ChangeState(player.moveState);
        }

    }
}
