using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    public Rigidbody2D rb {get; private set;}
    public Animator ani {get; private set;}
    public PlayerInputSet input {get; private set;}


    //Player States
    public Player_IdleStates idleState {get; private set;}
    public Player_MoveState moveState {get; private set;}
    public Player_JumpState jumpState {get; private set;}
    public Player_FallState fallState {get; private set;}
    public Player_WallSlide wallSlideState {get; private set;}
    public Player_WallJump wallJumpState {get; private set;}
    public Plaer_DashState dashState {get; private set;}
    public Player_BasicAttackState basicAttackState {get; private set;}
    public Player_JumpAttackState jumpAttackState {get; private set;}


    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1f;
    private Coroutine queuedAttackCo;



    [Header("Movement details")]
    public float moveSpeed = 7f;
    public float jumpForce = 5f;
    public Vector2 wallJumpForce;

    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;

    
    
    [Range(0,1)]
    public float inAirMoveMultiplier = 0.7f;

    [Range(0,1)]
    public float wallSlideSlowMultiplier = 0.3f;
    public Vector2 movementInput {get; private set;}
    public bool facingRight {get; private set;} = true;
    public int facingDirection {get; private set;} = 1;


    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDectected {get; private set;}
    public bool wallDetected {get; private set;}

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleStates(stateMachine, "idle", this);
        moveState = new Player_MoveState(stateMachine, "move", this);
        jumpState = new Player_JumpState(stateMachine, "jumpFall", this);
        fallState = new Player_FallState(stateMachine, "jumpFall", this);
        wallSlideState = new Player_WallSlide(stateMachine, "wallSlide", this);
        wallJumpState = new Player_WallJump(stateMachine, "jumpFall", this);
        dashState = new Plaer_DashState(stateMachine, "dash", this);
        basicAttackState = new Player_BasicAttackState(stateMachine, "basicattack", this);  
        jumpAttackState = new Player_JumpAttackState(stateMachine, "jumpAttack", this);
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
        HandleCollisionDetection();
    }

    public void EnterAttackStateWithDelay(){
        if(queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);
        queuedAttackCo = StartCoroutine(ENterAttackStateWithDelayCo());
    }
    

    private IEnumerator ENterAttackStateWithDelayCo(){
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }





    public void SetVelocity(float xVelocity, float yVelocity){
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandelFlip(xVelocity);
    }

    private void HandelFlip(float xVelocity){
        if (xVelocity > 0 && !facingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180f, 0);
        facingDirection *= -1;
    }

    private void HandleCollisionDetection(){
        groundDectected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, WhatIsGround) 
                && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDirection, wallCheckDistance, WhatIsGround);

    }


    public void CallAnimationTrigger(){
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(primaryWallCheck.position,primaryWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));
        Gizmos.DrawLine(secondaryWallCheck.position,secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }
}
