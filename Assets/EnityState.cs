using UnityEngine;

public abstract class EnityState 
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTime;
    protected bool triggerCalled;

    public EnityState(StateMachine stateMachine, string animBoolName, Player player){
        this.stateMachine = stateMachine; 
        this.animBoolName = animBoolName;
        this.player = player;
        //anim = player.GetComponentInChildren<Animator>();
        anim = player.ani;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter(){
      anim.SetBool(animBoolName, true);
      triggerCalled = false;
    }

    public virtual void Update(){

        stateTime -= Time.deltaTime;
       anim.SetFloat("yVelocity", rb.linearVelocity.y);

       if(input.Player.Dash.WasPressedThisFrame() && CanDash())
       {
            stateMachine.ChangeState(player.dashState);
       }
    }

    public virtual void Exit(){
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger(){
        triggerCalled = true;
    }


    private bool CanDash(){

        if(player.wallDetected)
            return false;
        
        if(stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}
