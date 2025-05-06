using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static MonsterSpawnManager Instance { get; private set; }
    
    [Header("Spawn Settings")]
    public GameObject[] monsterPrefabs;
    public float spawnInterval = 30f;
    public float brazierFireDuration = 3f;
    
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    private float timeSinceLastSpawn = 0f;
    
    private bool spawnMonsters = true;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        // Find spawn points
        spawnPoints.AddRange(FindObjectsByType<SpawnPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None));
        
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points found in the scene");
        }
        
        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("Monster prefab(s) not assigned");
        }
    }
    
    private void Update()
    {
        // Time-based spawning
        timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn >= spawnInterval && spawnPoints.Count > 0)
        {
            SpawnMonster();
            timeSinceLastSpawn = 0f;
        }
    }
    
    private void SpawnMonster()
    {
        SpawnPoint chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject monsterPrefab = GetRandomMonsterPrefab();
        
        // Spawn the monster
        if (monsterPrefab != null && spawnMonsters)
        {
            Instantiate(monsterPrefab, chosenSpawnPoint.transform.position, 
                        chosenSpawnPoint.transform.rotation);
            
            chosenSpawnPoint.ActivateBrazier(brazierFireDuration);
            Debug.Log($"Monster spawned at {chosenSpawnPoint.name}");
        }
        else
        {
            Debug.LogError("Monster prefab is not assigned");
        }
    }
    
    private GameObject GetRandomMonsterPrefab()
    {
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        return monsterPrefabs[randomIndex];
    }
    
    public void StopSpawning()
    {
        spawnMonsters = false;
        Debug.Log("Monster spawning stopped");

    }
}
