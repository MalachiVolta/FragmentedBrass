using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float travelSpeed = 10f;

    public void Homing(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = travelSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Hit(20);
        Destroy(gameObject);
    }
}
