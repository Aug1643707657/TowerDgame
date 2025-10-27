using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    private int waypointIndex = 0;
    private Transform target;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (PathPoints.points == null || PathPoints.points.Length == 0)
        {
            Debug.LogError("[Enemy] No PathPoints found! Make sure a Path object with PathPoints.cs is in your scene.");
            enabled = false;
            return;
        }

        target = PathPoints.points[0];
        Debug.Log($"[Enemy] {gameObject.name} spawned with {maxHealth} HP and target {target.name}");
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= PathPoints.points.Length - 1)
        {
            ReachGoal();
            return;
        }

        waypointIndex++;
        target = PathPoints.points[waypointIndex];
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"[Enemy] {gameObject.name} took {amount} damage. Remaining HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"[Enemy] {gameObject.name} died.");

        // ✅ Add score when this enemy dies
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(10); // add 10 points per kill
            Debug.Log("[Enemy] +10 Score awarded!");
        }
        else
        {
            Debug.LogWarning("[Enemy] GameManager not found! No score added.");
        }

        Destroy(gameObject);
    }

    void ReachGoal()
    {
        Debug.Log($"[Enemy] {gameObject.name} reached the goal and will be destroyed.");
        Destroy(gameObject);
    }

    // Optional: visualize path in the Scene view
    void OnDrawGizmosSelected()
    {
        if (PathPoints.points == null || PathPoints.points.Length < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < PathPoints.points.Length - 1; i++)
        {
            if (PathPoints.points[i] != null && PathPoints.points[i + 1] != null)
                Gizmos.DrawLine(PathPoints.points[i].position, PathPoints.points[i + 1].position);
        }
    }
}




