using UnityEngine;

/// <summary>
/// Needs to know all the spells that exists to update projectile
/// </summary>

public class SpellProjectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer projectileSprite;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hitDistance;

    private SpellData _spell;
    private MonsterController _targetBicho;

    public void Initialize(SpellData spell, MonsterController target)
    {
        
        _spell = spell;
        _targetBicho = target;
        
        if (projectileSprite)
        {
            projectileSprite.sprite = spell.SpellSprite;
        }
        
    }

    private void Update()
    {
        if (!_targetBicho)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = _targetBicho.GetTargetPosition();

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= hitDistance)
        {
            _targetBicho.DefeatMonsterwithSpell(_spell);
            Destroy(gameObject);
        }
    }
    
}
