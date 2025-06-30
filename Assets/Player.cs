using UnityEngine;

public class Player : MonoBehaviour
{
    //private Rigidbody2D rb;
    private PlayerInputSet input;
    public Player_IdleStates idleState {get; private set;}

    public Player_MoveState moveState {get; private set;}

    private StateMachine stateMachine;

    public Vector2 movementInput {get; private set;}

    private void Awake(){
        //rb = GetComponent<Rigidbody2D>();
        input = new PlayerInputSet();

        stateMachine = new StateMachine();
        idleState = new Player_IdleStates( stateMachine, "IdleState", this);
        moveState = new Player_MoveState(stateMachine, "MoveState", this);
    }

    private void OnEnable(){
        input.Enable();

        input.Player.Movement.performed += ctx => { movementInput = ctx.ReadValue<Vector2>(); };

        input.Player.Movement.canceled += ctx => { movementInput = Vector2.zero; };
    }

    private void OnDisable(){
        input.Disable();
    }
    
    private void Start(){
        stateMachine.Initialize(idleState);
    }

    private void Update(){
        stateMachine.UpdateActiveState();
    }
    
}
