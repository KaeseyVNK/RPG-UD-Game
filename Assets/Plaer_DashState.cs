using UnityEngine;

public class Plaer_DashState : EnityState
{
    private float originalGravityScale;
    private int dashDir;
    

    public Plaer_DashState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTime = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        dashDir = player.movementInput.x != 0 ? (int)player.movementInput.x : player.facingDirection;
    }

    public override void Update()
    {
        base.Update();
        CancelDahsIfNeeded();
        player.SetVelocity(dashDir * player.dashSpeed, 0);


        if(stateTime < 0)
        {
            if(player.groundDectected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDahsIfNeeded()
    {
        if(player.wallDetected)
        {
            if(player.groundDectected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}   
