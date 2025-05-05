using UnityEngine;
using UnityEngine.UI;

public class RuneTimer : MonoBehaviour
{
    public Image runeTimerFiller;

    void Start()
    {
        runeTimerFiller.fillAmount = 0f;
    }

    // Checks the timer and ensures that the image fills up as the timer increases
    private void Update()
    {
        float gameElapsedTime = GameManager.Instance.GetElapsedTime();
        float runeDuration = GameManager.Instance.GetGameDuration();
        float fillAmount = Mathf.Clamp01(gameElapsedTime / runeDuration);
        
        runeTimerFiller.fillAmount = fillAmount;
    }
}
