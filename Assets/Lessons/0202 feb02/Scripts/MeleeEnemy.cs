using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackRange.transform.position, attackRange.radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(attackRange.tagToCheck))
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(ATK);
                }
            }
        }
        
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
