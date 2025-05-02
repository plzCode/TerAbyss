using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;

    [Header("Animation")]
    public Animator animator;
    public PlayerStateMachine stateMachine;

    [Header("Player State")]
    public PlayerIdleState playerIdleState { get; private set; }
    public PlayerSitState playerSitState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");
        playerSitState = new PlayerSitState(this, stateMachine, "Sit");
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        stateMachine.Initialize(playerIdleState);
    }

    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(animator.GetBool("Sit"))
            {
                stateMachine.ChangeState(playerIdleState);
            }
            else
            {
                stateMachine.ChangeState(playerSitState);
            }
        }
    }

    void Move()
    {
        if (animator.GetBool("Sit"))
        {
            return;
        }
        // 입력값 받기
        float horizontal = Input.GetAxis("Horizontal"); // A, D
        float vertical = Input.GetAxis("Vertical");     // W, S

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // 카메라 방향 기준 이동
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 땅에 있으면 중력 리셋
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float speed = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("Speed", speed);
    }
}