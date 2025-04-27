using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stoppingDistance = 2.5f;
    
    private Transform playerTransform;
    private bool isMoving = true;
    
    private void Start()
    {
        // Acquire player location
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("MonsterMovement: Player not found");
            enabled = false;
        }
    }
    
    private void Update()
    {
        if (!isMoving || playerTransform == null)
            return;
            
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer > stoppingDistance)
        {
            // Move towards the player position
            transform.position = Vector3.MoveTowards(
                transform.position, 
                playerTransform.position, 
                moveSpeed * Time.deltaTime
            );
            
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;
            if (directionToPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
        else
        {
            // Stop once close enough
            isMoving = false;
        }
    }
}
