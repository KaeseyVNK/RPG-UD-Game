using UnityEngine;

public class Player_MoveState : EnityState
{
    public Player_MoveState(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {

    }


    public override void Update()
    {
        base.Update();
        if (player.movementInput.x == 0 )
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
