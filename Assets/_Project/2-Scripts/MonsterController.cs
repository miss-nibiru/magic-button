using UnityEngine;

/// <summary>
/// this script controls the monster movement and how it communicates with the grid and locations
/// </summary>
public class MonsterController : MonoBehaviour
{
    [SerializeField] private MainGrid mainGrid;
    
    [SerializeField] private int currentColumn;
    [SerializeField] private int dangerColumn;
    
    [SerializeField] private PlayerHealth playerHealth;
    
    [SerializeField] private float moveSpeed;
    private Vector3 _targetPosition;

    private void Start()
    {
        if (!mainGrid)
        {
            Debug.LogError("MainGrid reference is missing!");
            return;
        }
        
        _targetPosition = mainGrid.GetColumnLocation(currentColumn);
        transform.position = _targetPosition; // Start at the initial target position
        
    }

    private void Update()
    {
        if (!mainGrid) return;

        // Move towards the target position -- this can be used instead of lerp
        transform.position = Vector3.MoveTowards
            (transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        // Check if we've reached the target position
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            if (dangerColumn >= currentColumn)
            {
                Debug.Log("Monster reached the player on column " + currentColumn);
                playerHealth.TakeDamage(1); // Assuming the monster does 1 damage for now, change when monsters are created
                enabled = false;
                return;
            }

            currentColumn--;
            _targetPosition = mainGrid.GetColumnLocation(currentColumn);
        }
        
    }
    
    
        
}
