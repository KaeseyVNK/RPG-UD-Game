using UnityEngine;

public class StateMachine
{
    public EnityState currentState {get; private set;}

    public void Initialize(EnityState startingState){
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EnityState newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState(){
        currentState.Update();
    }
    
}
