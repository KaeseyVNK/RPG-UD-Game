using UnityEngine;

public class Player_GroundState : EnityState
{
    public Player_GroundState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {

    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y  < 0 && !player.groundDectected)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.Player.Jump.WasPressedThisFrame())
        {
           stateMachine.ChangeState(player.jumpState);
        }
        
        if(input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.basicAttackState);
        }

    }
}
