using UnityEngine;

public class Player_JumpAttackState : EnityState
{
    private bool touchGround;
    public Player_JumpAttackState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        touchGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(player.groundDectected && !touchGround){
            touchGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if(triggerCalled && player.groundDectected){
            stateMachine.ChangeState(player.idleState);
        }
    }
    
}
