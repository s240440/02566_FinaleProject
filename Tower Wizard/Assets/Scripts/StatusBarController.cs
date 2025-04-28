using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusBarController : MonoBehaviour
{
    [Header("Health")]
    public int health = 3;
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
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;


    public void SubtractHealth(int amount)
    {
        health -= amount;
        Debug.Log("Health subtracted to: " + health);
        if (health <= 0)
        {
            Debug.Log("Game Over");
            // Add game over logic here
        }
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
            LaunchProjectile();
        }
        else if (lastMove == "arms_up" && movename == "arms_front")
        {
            moveSlider.value = 2;
        }

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

    private void LaunchProjectile()
    {
        Debug.Log("Launch projectile function started");
        if (projectilePrefab1 == null || projectileSpawnPoint == null) return;
        Debug.Log("Initiating projectile");
        GameObject go = Instantiate(projectilePrefab1, projectileSpawnPoint.position, projectileSpawnPoint.rotation);  // Give it speed 
        Debug.Log(" projectile initiated");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = 3;
        healthSlider.value = 3;
        moveSlider.maxValue = 2;
        moveSlider.value = 0;
        moveText.text = "Move: " + "None";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
