using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] int damage = 30;
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
        enemy.Hit(damage);
        Destroy(gameObject);
    }
}
