using UnityEngine;

public class Player_WallSlide : EnityState
{
    public Player_WallSlide(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }


    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if(input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if(!player.wallDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.groundDectected)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }    
    }


    private void HandleWallSlide(){
        if(player.movementInput.y < 0)
        {
            player.SetVelocity(player.movementInput.x, rb.linearVelocity.y);
        }
        else
        {
            player.SetVelocity(player.movementInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);
        }
    }
}
