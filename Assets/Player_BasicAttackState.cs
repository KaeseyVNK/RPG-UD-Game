using UnityEngine;

public class Player_BasicAttackState : EnityState
{
    private float attackVelocityTimer;


    private const int firstComboLimit = 1;
    private int attackDir;
    private int comboIndex =1;
    private int comboLimit = 3;


    private float lastTimeAttacked;
    private bool comboAttackQueued;


    public Player_BasicAttackState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
        if(comboLimit > player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        attackDir = player.movementInput.x != 0 ? (int)player.movementInput.x : player.facingDirection;

        anim.SetInteger("basixAttackIndex", comboIndex);
        ApplyAttackVelocity();
  
    }

    private void ResetComboIndexIfNeeded()
    {
        if(Time.time >= lastTimeAttacked + player.comboResetTime)
            comboIndex = firstComboLimit;

        if (comboIndex > comboLimit)
            comboIndex = firstComboLimit;
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();


        if(input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();
        if(triggerCalled)
                HandleStateExit();  
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit(){      
        if(comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);  
    }

    private void QueueNextAttack(){
        if(comboIndex < comboLimit)
            comboAttackQueued = true;
    }

    private void HandleAttackVelocity(){
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }


    private void ApplyAttackVelocity(){

        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackDir * attackVelocity.x, attackVelocity.y);
    }
}
