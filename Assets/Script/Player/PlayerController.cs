using UnityEngine;
using System.Collections;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Character
{
    [Header("Movement Settings")]
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;

    [Header("Animation")]
    public PlayerStateMachine stateMachine;
    public bool isBusy = false;

    [Header("Player State")]
    public PlayerIdleState playerIdleState { get; private set; }
    [Header("Skill")]
    public Skill[] skillSlots = new Skill[8];
    public PlayerSkillState[] skillStates = new PlayerSkillState[8];
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;

    [Header("For Mobile")]
    public DynamicJoystick joystick;
    public bool isMobile = false;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");

        skillSlots[0] = new BulletSKill
        {
            bulletPrefab = bulletPrefab,
            bulletSpeed = 15f,
            firePoint = firePoint,
            Cooldown = 2f
        };
        skillSlots[1] = new BulletSKill
        {
            bulletPrefab = bulletPrefab2,
            bulletSpeed = 15f,
            firePoint = firePoint,
            Cooldown = 2f
        };

        // ✅ playerSkillStates 배열 초기화
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (skillSlots[i] != null)
            {
                skillStates[i] = new PlayerSkillState(this, stateMachine, $"Skill_0{i + 1}", skillSlots[i]);
            }
        }

    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        stateMachine.Initialize(playerIdleState);
    }

    protected override void Update()
    {
        base.Update();
        Move();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isMobile)
        {
            SkillAttack_1();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isMobile)
        {
            SkillAttack_2();
        }
    }

    void Move()
    {
        if (isBusy)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal"); // A, D
        float vertical = Input.GetAxis("Vertical");     // W, S
        // 입력값 받기
        if (isMobile)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;  
        }

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
    public void SkillAttack_1()
    {
        if (skillSlots[0] != null && skillSlots[0].CanActivate())
        {
            stateMachine.ChangeState(skillStates[0]);
        }
    }
    public void SkillAttack_2()
    {
        if (skillSlots[1] != null && skillSlots[1].CanActivate())
        {
            stateMachine.ChangeState(skillStates[1]);
        }
    }
}