using UnityEngine;
public abstract class Skill
{
    public float Cooldown;
    public float LastUsedTime;

    public bool CanActivate()
    {
        return Time.time >= LastUsedTime + Cooldown;
    }

    public void Use(Character owner)
    {
        if (CanActivate())
        {            
            LastUsedTime = Time.time;
            Activate(owner);
        }
    }

    public abstract void Activate(Character owner);
}

public class BulletSKill : Skill
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;

    public override void Activate(Character owner)
    {
        // 1. �߻� ��ġ�� �������� �ʾҴٸ� owner ��ġ �������� ����
        Vector3 spawnPosition = firePoint != null ? firePoint.position : owner.transform.position + owner.transform.forward * 1f + Vector3.up * 1f;
        Quaternion rotation = Quaternion.LookRotation(owner.transform.forward);

        // 2. �߻�ü ����
        GameObject fireball = GameObject.Instantiate(bulletPrefab, spawnPosition, rotation);

        // 3. Rigidbody�� �̿��� ������ ����
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = owner.transform.forward * bulletSpeed;
        }

        // 4. (����) ���� �ð� �� �ڵ� �ı�
        GameObject.Destroy(fireball, 5f);
    }
}
