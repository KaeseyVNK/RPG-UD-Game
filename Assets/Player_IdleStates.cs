using UnityEngine;

public class Player_IdleStates : EnityState
{
    public Player_IdleStates(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {

    }

    public override void Update()
    {
        base.Update();
        if (player.movementInput.x != 0 )
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
