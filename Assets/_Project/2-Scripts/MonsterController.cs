using UnityEngine;

/// <summary>
/// this script controls the monster movement and how it communicates with the grid and locations
/// </summary>
public class MonsterController : MonoBehaviour

{
    [SerializeField] private MainGrid mainGrid;
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private int currentColumn;
    private Vector3 _targetPosition;
    
    public int CurrentColumn => currentColumn;
    public int DangerZone => monsterData.DangerZone;
    public bool CanBeTargeted => gameObject.activeInHierarchy && enabled; //it has to be created already

    private void Start()
    {
        if (!mainGrid)
        {
            Debug.LogError("MainGrid reference is missing!");
            return;
        }
        
        if (!monsterData)
        {
            Debug.LogError("MonsterData reference is missing!");
            return;
        }

        currentColumn = monsterData.StartingColumn;
        _targetPosition = mainGrid.GetColumnLocation(currentColumn);
        transform.position = _targetPosition; // Start at the initial target position
        
    }

    private void Update()
    {
        if (!mainGrid) return;

        // Move towards the target position -- this can be used instead of lerp (MoveTowards)
        transform.position = Vector3.MoveTowards
            (transform.position, _targetPosition, monsterData.MonsterSpeed * Time.deltaTime);

        // Check if we've reached the target position
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            if (monsterData.DangerZone >= currentColumn)
            {
                Debug.Log("Monster reached the player on column " + currentColumn);
                playerHealth.TakeDamage(monsterData.MonsterDamage); //each monster has its own damage so player takes different amount of damage each time
                enabled = false;
                return;
            }

            currentColumn--;
            _targetPosition = mainGrid.GetColumnLocation(currentColumn);
        }
        
    }

    public void InitializeMonster(MainGrid mainGrid, PlayerHealth playerHealth)
    {
        
        this.mainGrid = mainGrid;
        this.playerHealth = playerHealth;
        
    }

    public bool DefeatMonsterwithSpell(SpellData spell)
    {

        if (spell == monsterData.MonsterWeakness)
        {

            Debug.Log(monsterData.MonsterName + " was defeated by the spell " + spell.SpellName);
            Destroy(gameObject);
            return true;

        }

        Debug.Log(monsterData.MonsterName + " is resistant to " + spell.SpellName);
        return false;

    }
    
    
        
}
