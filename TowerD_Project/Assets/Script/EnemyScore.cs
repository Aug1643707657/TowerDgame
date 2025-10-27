using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    [Header("Score Settings")]
    public int scoreValue = 10; // points to add when this enemy dies

    private bool hasGivenScore = false; // prevents double-counting

    void OnDestroy()
    {
        // Only add score if the GameManager exists and score hasn't been added yet
        if (!hasGivenScore && GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
            hasGivenScore = true;
            Debug.Log($"[EnemyScore] +{scoreValue} points added for {gameObject.name}");
        }
        else if (GameManager.Instance == null)
        {
            Debug.LogWarning("[EnemyScore] GameManager not found! Score not added.");
        }
    }
}

