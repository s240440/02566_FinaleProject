using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 180f;
    
    [Header("Events")]
    public UnityEvent OnGameEnd;
    
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
        StartGame();
    }
    
    private void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime >= gameDuration)
            {
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
        OnGameEnd?.Invoke();
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
}
