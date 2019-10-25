using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    protected static PlayerController s_Instance;
    public static PlayerController instance { get { return s_Instance; } }

    private float speed;

    public float maxForwardSpeed = 8f;        // How fast Ellen can run.
    public float gravity = 20f;               // How fast Ellen accelerates downwards when airborne.
    public float jumpSpeed = 10f;             // How fast Ellen takes off when jumping.
    public float minTurnSpeed = 400f;         // How fast Ellen turns when moving at maximum speed.
    public float maxTurnSpeed = 1200f;        // How fast Ellen turns when stationary.
    public float idleTimeout = 5f;            // How long before Ellen starts considering random idles.
    public bool canAttack;                    // Whether or not Ellen can swing her staff.

    protected float m_DesiredForwardSpeed;         // How fast Ellen aims be going along the ground based on input.
    protected float m_ForwardSpeed;                // How fast Ellen is currently going along the ground.
    protected float m_VerticalSpeed;               // How fast Ellen is currently moving up or down.
    protected PlayerInput m_Input;                 // Reference used to determine how Ellen should move.
    protected Animator m_Animator;                 // Reference used to make decisions based on Ellen's current animation and to set parameters.

    const float k_GroundAcceleration = 20f;
    const float k_GroundDeceleration = 25f;

    private float m_LookAngle;
    [Range(0f, 10f)] [SerializeField] private float m_TurnSpeed = 1.5f;
    private Quaternion m_TransformTargetRot;

    // Animator Parameter
    readonly int m_HashForwardSpeed = Animator.StringToHash("Speed");


    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(m_Input.MoveInput.sqrMagnitude, 0f); }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_Animator = GetComponent<Animator>();

        s_Instance = this;

        m_TransformTargetRot = transform.localRotation;
    }
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lookAtMouse();
        fixedMoveCharacter();
    }

    // 캐릭터가 마우스를 바라봄
    void lookAtMouse()
    {
        //Vector3 mouse = Input.mousePosition;

        //Debug.Log("mouse.x : " + mouse.x + "mouse.y : " + mouse.y + "mouse.z : " + mouse.z);

        //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, -Camera.main.transform.position.z));

        //Debug.Log("mouseWorld.x : " + mouseWorld.x + "mouseWorld.y : " + mouseWorld.y + "mouseWorld.z : " + mouseWorld.z);

        Vector3 lookTarget = new Vector3(0, 0, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookTarget = hit.point;
            lookTarget.y = transform.position.y;
        }
        transform.LookAt(lookTarget);
    }

    // 캐릭터 상하좌우 이동
    void fixedMoveCharacter()
    {
        if(Input.anyKey)
        {
            if(Input.GetKey(KeyCode.W))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.0f, 0.0f, 10.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.0f, 0.0f, -10.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-10.0f, 0.0f, 0.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(10.0f, 0.0f, 0.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
        }
    }

    void CalculateForwardMovement()
    {
        // Cache the move input and cap it's magnitude at 1.
        Vector2 moveInput = m_Input.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();

        // Calculate the speed intended by input.
        m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

        // Determine change to speed based on whether there is currently any move input.
        float acceleration = IsMoveInput ? k_GroundAcceleration : k_GroundDeceleration;

        // Adjust the forward speed towards the desired speed.
        m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);

        // Set the animator parameter to control what animation is being played.
        m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
    }
}
