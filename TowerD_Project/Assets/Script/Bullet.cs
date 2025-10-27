using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    public void Seek(Transform _target)
    {
        if (_target == null)
        {
            Debug.LogWarning("[Bullet] Seek() called with null target!");
            return;
        }

        target = _target;
        Debug.Log($"[Bullet] Seeking target: {target.name}");
    }

    void Update()
    {
        if (target == null)
        {
            Debug.Log("[Bullet] Lost target. Destroying bullet.");
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Debug.Log($"[Bullet] Hit target: {target.name}");

    EnemyHealth2D health = target.GetComponent<EnemyHealth2D>();
    if (health != null)
    {
        health.TakeDamage(10f); // <- make sure this line is called
    }
    else
    {
        Debug.LogWarning("[Bullet] Target does not have EnemyHealth2D.");
    }

    Destroy(gameObject);
    }
}





