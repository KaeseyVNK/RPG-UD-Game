using UnityEngine;

public abstract class EnityState 
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string StateName;

    public EnityState(StateMachine stateMachine, string stateName, Player player){
        this.stateMachine = stateMachine; 
        this.StateName = stateName;
        this.player = player;
    }

    public virtual void Enter(){
       Debug.Log("Entering state: " + StateName);
    }

    public virtual void Update(){
        Debug.Log("Updating state: " + StateName);
    }

    public virtual void Exit(){
        Debug.Log("Exiting state: " + StateName);
    }
}
