using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float rotationSpeed = 5f;  // ī�޶� ȸ�� �ӵ�
    public float minRotationX = -30f; // ī�޶� �ּ� ȸ�� ���� (��)
    public float maxRotationX = 60f;  // ī�޶� �ִ� ȸ�� ���� (�Ʒ�)
    public float followDistance = 5f; // ī�޶�� �÷��̾� �� �⺻ �Ÿ�
    public float height = 2f;         // ī�޶� ����
    public float collisionBuffer = 0.3f; // ���� �浹�� ���� ���� �Ÿ�
    public LayerMask collisionLayer; // �浹�� �˻��� ���̾� (�� ���̾�)

    [Header("References")]
    public Transform player;  // �÷��̾��� Transform
    private Transform playerTransform;
    private float currentRotationX = 0f;

    void Start()
    {
        playerTransform = player;
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // Ŀ�� ����
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        // ���콺 ���������� ī�޶� ȸ��
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

        // Y�� (�¿�) ȸ��
        transform.RotateAround(playerTransform.position, Vector3.up, horizontal);

        // X�� (����) ȸ��, ���� ����
        currentRotationX += vertical;
        currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);

        // ī�޶� ȸ�� ����
        Quaternion rotation = Quaternion.Euler(currentRotationX, transform.eulerAngles.y, 0f);
        Vector3 direction = new Vector3(0f, 0f, -followDistance);

        // ȸ���� �������� ī�޶� ��ġ ���
        Vector3 targetPosition = playerTransform.position + rotation * direction + new Vector3(0f, height, 0f);

        // Raycast�� �� �˻� (LayerMask�� ����Ͽ� Ư�� ���̾ �˻�)
        RaycastHit hit;
        float currentFollowDistance = followDistance;

        // ���� �浹�ϴ��� Ȯ��
        Vector3 raycastStartPosition = playerTransform.position + new Vector3(0f, height, 0f);
        Vector3 raycastDirection = targetPosition - raycastStartPosition;

        if (Physics.Raycast(raycastStartPosition, raycastDirection, out hit, followDistance, collisionLayer))
        {
            // ���� �ִٸ� ī�޶� ���� ����������� �Ÿ� ����
            currentFollowDistance = Mathf.Clamp(hit.distance - collisionBuffer, 2f, followDistance);
        }

        // ������ �ּ� �Ÿ��� ������ ����
        if (currentFollowDistance < collisionBuffer)
        {
            currentFollowDistance = collisionBuffer;  // �ּ� �Ÿ� ����
        }

        // ���������� ī�޶� ��ġ ������Ʈ
        targetPosition = playerTransform.position + rotation * new Vector3(0f, 0f, -currentFollowDistance) + new Vector3(0f, height, 0f);
        transform.position = targetPosition;
        transform.LookAt(playerTransform.position + new Vector3(0f, height, 0f));
    }
}