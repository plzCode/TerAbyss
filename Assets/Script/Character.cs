using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100;
    protected float currentHealth;

    [Header("Combat")]
    public Transform firePoint;       // ����ü �߻� ��ġ
    public float moveSpeed = 5f;

    [Header("Animation")]
    public Animator animator;

    [Header("Effects")]
    public GameObject hitEffect;
    public GameObject deathEffect;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        // �ڽ� Ŭ�������� �ʱ�ȭ �ʿ� �� �������̵�
    }

    protected virtual void Update()
    {
        // �⺻ ĳ���� ������Ʈ ���� (�ʿ� ��)
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP: {currentHealth}");

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public virtual void PlayAnimation(string triggerName)
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }

    public virtual Vector3 GetFirePoint()
    {
        return firePoint != null ? firePoint.position : transform.position + transform.forward * 1f + Vector3.up * 1f;
    }

    public virtual Vector3 GetForwardDirection()
    {
        return transform.forward;
    }
}