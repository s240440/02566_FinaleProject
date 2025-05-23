using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stoppingDistance = 2.5f;

    private Transform playerTransform;
    private StatusBarController statusBarController;
    private MonsterHealth monsterHealth;


    private void Start()
    {
        GameObject statusBarObject = GameObject.FindGameObjectWithTag("GameController");
        statusBarController = statusBarObject.GetComponent<StatusBarController>();
        monsterHealth = GetComponent<MonsterHealth>();
        if (monsterHealth == null)
        {
            Debug.LogError("MonsterHealth component not found on this GameObject.");
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found");
            enabled = false;
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= stoppingDistance)
        {
            if (statusBarController != null)
            {
                statusBarController.SubtractHealth(1);
            }
            monsterHealth.DamageMonster(999);
        }
        else
        {
            // Move towards player
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                moveSpeed * Time.deltaTime
            );

            // Make monster face player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;
            if (directionToPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
    }
}
