using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private float gameDuration;
    
    [Header("Skybox Settings")]
    [SerializeField] private Material initialSkybox;
    [SerializeField] private Material endGameSkybox;
    
    [Header("Events")]
    public UnityEvent OnGameEnd;
    public GameObject Rewardplushy;
    public Music MusicController;
    
    private float elapsedTime = 0f;
    private bool isGameRunning = false;
    
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
    
    private void Start()
    {
    	if (initialSkybox != null)
        {
            RenderSettings.skybox = initialSkybox;
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
                // Endgame win
                EndGame();
            }
        }
    }
    
    public void StartGame()
    {
        elapsedTime = 0f;
        isGameRunning = true;
    }
    
    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("GAME ENDED - Time expired!");
        ChangeSkyboxToEndGame();
        OnGameEnd?.Invoke();
        MusicController.PlayWinSound();
    }
    
    private void ChangeSkyboxToEndGame()
    {
        if (endGameSkybox != null)
        {
            RenderSettings.skybox = endGameSkybox;
            DynamicGI.UpdateEnvironment();
            Rewardplushy.SetActive(true);
            Debug.Log("Changed to endgame skybox");
        }
    }
    
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
}
