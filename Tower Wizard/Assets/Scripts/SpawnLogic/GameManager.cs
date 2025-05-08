using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private float gameDuration;
    [SerializeField] private float restartDelay = 5f;
    
    [Header("Skybox Settings")]
    [SerializeField] private Material initialSkybox;
    [SerializeField] private Material gameWinSkybox;
    [SerializeField] private Material playerDeathSkybox;
    
    
    [Header("Events")]
    public UnityEvent OnGameEnd;
    public GameObject Rewardplushy;
    public Music MusicController;
    
    private float elapsedTime = 0f;
    private bool isGameRunning = false;
    private bool isRestartScheduled = false;
    private GameEndReason endReason = GameEndReason.None;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    // Subscribing to events for player death
    private void OnEnable()
    {
        StatusBarController.OnPlayerDeath += HandlePlayerDeath;
    }
    
    private void OnDisable()
    {
        StatusBarController.OnPlayerDeath -= HandlePlayerDeath;
    }
    
    private void Start()
    {
    	if (initialSkybox != null)
        {
            RenderSettings.skybox = initialSkybox;
            DynamicGI.UpdateEnvironment();
        }
        StartGame();
    }
    
    private void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime >= gameDuration)
            {
                EndGame(GameEndReason.TimeUp);
            }
        }
    }
    
    public void StartGame()
    {
        elapsedTime = 0f;
        isGameRunning = true;
        endReason = GameEndReason.None;
        isRestartScheduled = false;
        
        if (initialSkybox != null)
        {
            RenderSettings.skybox = initialSkybox;
            DynamicGI.UpdateEnvironment();
        }
    }
    
    private void HandlePlayerDeath()
    {
        if (isGameRunning)
        {
            EndGame(GameEndReason.PlayerDeath);
        }
    }
    
    public void EndGame(GameEndReason reason)
    {
        if (!isGameRunning) return; // Prevent multiple calls
        
        isGameRunning = false;
        endReason = reason;
        
        switch (reason)
        {
            case GameEndReason.TimeUp:
                Debug.Log("GAME ENDED - Time expired");
                MusicController.PlayWinSound();
                ChangeSkybox(gameWinSkybox);
                break;
                
            case GameEndReason.PlayerDeath:
                Debug.Log("GAME ENDED - Player died");
                MusicController.PlayLoseSound();
                ChangeSkybox(playerDeathSkybox);
                break;
        }
        
        OnGameEnd?.Invoke();
        
        if (!isRestartScheduled)
        {
            isRestartScheduled = true;
            StartCoroutine(RestartGameAfterDelay());
        }
    }
    
    private void ChangeSkybox(Material skybox)
    {
        if (skybox != null)
        {
            RenderSettings.skybox = skybox;
            DynamicGI.UpdateEnvironment();
        }
    }
    
    private IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        
        // Reload the scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        
        StartGame();
    }
    
    // Utility methods
    
    public float GetRemainingTime()
    {
        return Mathf.Max(0, gameDuration - elapsedTime);
    }
    
    public void PauseGame()
    {
        isGameRunning = false;
    }
    
    public void ResumeGame()
    {
        isGameRunning = true;
    }
    
    public float GetElapsedTime()
    {
    	return elapsedTime;
    }
    
    public float GetGameDuration()
    {
    	return gameDuration;
    }
    
    public GameEndReason GetGameEndReason()
    {
        return endReason;
    }
}

public enum GameEndReason
{
    None,
    TimeUp,
    PlayerDeath
}
