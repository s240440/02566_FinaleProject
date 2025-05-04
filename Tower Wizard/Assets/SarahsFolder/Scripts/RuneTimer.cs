using UnityEngine;
using UnityEngine.UI;

public class RuneTimer : MonoBehaviour
{
    public Image runeTimerFiller;
    public float timerDuration = 60f;

    private float timer;

    void Start()
    {
        timer = timerDuration;
        runeTimerFiller.fillAmount = 0f;
    }

    // Checks the timer and ensures that the image fills up as the timer increases
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            float fill = 1f - (timer / timerDuration);
            runeTimerFiller.fillAmount = fill;
        }
    }
}
