using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;     // ← multiple spawn points
    public int enemiesPerRound = 5;
    public float spawnInterval = 1f;

    [Header("Round Settings")]
    public int currentRound = 0;
    public TMP_Text roundText;
    public Button startRoundButton;

    private bool isSpawning = false;

    void Start()
    {
        UpdateRoundText();

        if (startRoundButton != null)
            startRoundButton.onClick.AddListener(StartNextRound);
        else
            Debug.LogWarning("[Spawner] StartRoundButton not assigned!");
    }

    public void StartNextRound()
    {
        if (isSpawning)
        {
            Debug.Log("[Spawner] Already spawning!");
            return;
        }

        currentRound++;
        UpdateRoundText();
        Debug.Log($"[Spawner] Starting round {currentRound}");

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;

        int totalEnemies = enemiesPerRound + (currentRound - 1) * 2;

        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        Debug.Log("[Spawner] Wave complete!");
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("[Spawner] Missing enemyPrefab!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("[Spawner] No spawn points assigned!");
            return;
        }

        // ✅ Pick a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawn = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, randomSpawn.position, Quaternion.identity);
        Debug.Log($"[Spawner] Spawned enemy at {randomSpawn.name}");
    }

    void UpdateRoundText()
    {
        if (roundText != null)
            roundText.text = $"Round: {currentRound}";
        else
            Debug.LogWarning("[Spawner] RoundText not assigned!");
    }
}



