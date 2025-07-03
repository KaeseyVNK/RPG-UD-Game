using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {

    }

    public override void Update()
    {
        base.Update();
        if (player.groundDectected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
