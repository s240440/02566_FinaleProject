using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusBarController : MonoBehaviour
{

    public static event System.Action OnPlayerDeath;

    [Header("Health")]
    public int health = 4;
    public Slider healthSlider;
    [Header("Move Bar")]
    public int movebar = 0;
    public Slider moveSlider;
    public TextMeshProUGUI moveText;
    private string lastMove;
    public float moveResetTime = 3f;
    private Coroutine resetMoveCoroutine;

    [Header("Projectile")]
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;

    // Shield 1 is the first one that should disappear
    public GameObject Shield1;
    public GameObject Shield2;
    public GameObject Shield3;
    public TextMeshProUGUI debugtext;

    // Audio
    private AudioClip damageClip;
    private AudioSource audioSource;


    public void SubtractHealth(int amount)
    {
        debugtext.text = "Subtracting health";
        health -= amount;
        healthSlider.value = health;
        PlayDamageSound();

        if (health == 3)
        {
            Shield1.SetActive(false);
        }
        else if (health == 2)
        {
            Shield2.SetActive(false);
        }
        else if (health == 1)
        {
            Shield3.SetActive(false);
        }
        else if (health <= 0)
        {
           // Debug.Log("Game Over");
            OnPlayerDeath?.Invoke();
        }

    }

    void PlayDamageSound()
    {
        if (damageClip != null && audioSource != null)
            audioSource.PlayOneShot(damageClip);
    }

    public void RegisterMove(string movename)
    // possible moves: arms_front, right_straight, arms_up, dab
    {
        moveText.text = "Move: " + movename;
        if (string.IsNullOrEmpty(lastMove))
        {
            moveSlider.value = 1;
        }
        else if (lastMove == "dab" && movename == "right_straight")
        {
            moveSlider.value = 2;
            LaunchProjectile(projectilePrefab1);
        }
        else if (lastMove == "arms_up" && movename == "arms_front")
        {
            moveSlider.value = 2;
            LaunchProjectile(projectilePrefab2);
        }

        // // For debug
        // moveSlider.value = 2;
        // LaunchProjectile();
        // // end


        lastMove = movename;

        if (resetMoveCoroutine != null)
        {
            StopCoroutine(resetMoveCoroutine);
        }
        resetMoveCoroutine = StartCoroutine(ResetMoveAfterDelay(moveResetTime));
    }

    private IEnumerator ResetMoveAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lastMove = null;
        moveText.text = "Move: None";
        moveSlider.value = 0;
    }

    private void LaunchProjectile(GameObject projectile)
    {
        Debug.Log("Launch projectile function started");
        if (projectile == null || projectileSpawnPoint == null) return;
        Debug.Log("Initiating projectile");
        GameObject go = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Debug.Log(" projectile initiated");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = 4;
        healthSlider.value = 4;
        moveSlider.maxValue = 2;
        moveSlider.value = 0;
        moveText.text = "Move: " + "None";

        // Load your damage sound from Resources folder:
        damageClip = Resources.Load<AudioClip>("Sounds/DM-CGS-29");

        if (damageClip == null)
        {
            Debug.LogError("Failed to load audio from Resources/Sounds/DM-CGS-29.wav. Check the path.");
        }

        // Add an AudioSource component dynamically or manually
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
