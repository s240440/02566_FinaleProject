using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static MonsterSpawnManager Instance { get; private set; }
    
    [Header("Spawn Settings")]
    public GameObject monsterPrefab;
    public float spawnInterval = 30f;
    
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
        
        // Spawn the monster
        if (monsterPrefab != null && spawnMonsters)
        {
            Instantiate(monsterPrefab, chosenSpawnPoint.transform.position, 
                        chosenSpawnPoint.transform.rotation);
            
            Debug.Log($"Monster spawned at {chosenSpawnPoint.name}");
        }
        else
        {
            Debug.LogError("Monster prefab is not assigned");
        }
    }
    
        public void StopSpawning()
    {
        spawnMonsters = false;
        Debug.Log("Monster spawning stopped");

    }
}
