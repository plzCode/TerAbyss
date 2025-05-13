using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float rotationSpeed = 5f;  // 카메라 회전 속도
    public float minRotationX = -30f; // 카메라 최소 회전 각도 (위)
    public float maxRotationX = 60f;  // 카메라 최대 회전 각도 (아래)
    public float followDistance = 5f; // 카메라와 플레이어 간 기본 거리
    public float height = 2f;         // 카메라 높이
    public float collisionBuffer = 0.3f; // 벽과 충돌할 때의 여유 거리
    public LayerMask collisionLayer; // 충돌을 검사할 레이어 (벽 레이어)

    [Header("References")]
    public Transform player;  // 플레이어의 Transform
    private Transform playerTransform;
    private float currentRotationX = 0f;

    void Start()
    {
        playerTransform = player;
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false; // 커서 숨김
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        // 마우스 움직임으로 카메라 회전
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

        // Y축 (좌우) 회전
        transform.RotateAround(playerTransform.position, Vector3.up, horizontal);

        // X축 (상하) 회전, 각도 제한
        currentRotationX += vertical;
        currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);

        // 카메라 회전 적용
        Quaternion rotation = Quaternion.Euler(currentRotationX, transform.eulerAngles.y, 0f);
        Vector3 direction = new Vector3(0f, 0f, -followDistance);

        // 회전된 방향으로 카메라 위치 계산
        Vector3 targetPosition = playerTransform.position + rotation * direction + new Vector3(0f, height, 0f);

        // Raycast로 벽 검사 (LayerMask를 사용하여 특정 레이어만 검사)
        RaycastHit hit;
        float currentFollowDistance = followDistance;

        // 벽과 충돌하는지 확인
        Vector3 raycastStartPosition = playerTransform.position + new Vector3(0f, height, 0f);
        Vector3 raycastDirection = targetPosition - raycastStartPosition;

        if (Physics.Raycast(raycastStartPosition, raycastDirection, out hit, followDistance, collisionLayer))
        {
            // 벽이 있다면 카메라가 벽에 가까워지도록 거리 조정
            currentFollowDistance = Mathf.Clamp(hit.distance - collisionBuffer, 2f, followDistance);
        }

        // 벽과의 최소 거리를 강제로 유지
        if (currentFollowDistance < collisionBuffer)
        {
            currentFollowDistance = collisionBuffer;  // 최소 거리 설정
        }

        // 최종적으로 카메라 위치 업데이트
        targetPosition = playerTransform.position + rotation * new Vector3(0f, 0f, -currentFollowDistance) + new Vector3(0f, height, 0f);
        transform.position = targetPosition;
        transform.LookAt(playerTransform.position + new Vector3(0f, height, 0f));
    }
}