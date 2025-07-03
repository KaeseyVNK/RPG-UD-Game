using UnityEngine;

public class Player_AirState : EnityState
{
    public Player_AirState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {

    }

    public override void Update(){
        base.Update();

        if(player.movementInput.x != 0){
            player.SetVelocity(player.movementInput.x * (player.inAirMoveMultiplier * player.moveSpeed), rb.linearVelocity.y);
        }

        if(input.Player.Attack.WasPressedThisFrame()){
            stateMachine.ChangeState(player.jumpAttackState);
        }

    }
}
