using UnityEngine;

public class Player_WallJump : EnityState
{
    public Player_WallJump(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.wallJumpForce.x * -player.facingDirection, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        if(rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if(player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

    }

}
