using System.Collections;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage;
    public float speed = 1.0f;
    public float duration = 1.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void InstantiateProjectile(Vector2 velocity)
    {
        rb.linearVelocity = velocity * speed;
        StartCoroutine(ProjectileTimer());
    }

    public IEnumerator ProjectileTimer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
            
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
