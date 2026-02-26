using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBar;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        
        if (healthBar == null)
            Debug.LogWarning($"HealthBar Image not assigned on {gameObject.name}");

        if (enemy == null)
            Debug.LogWarning($"Enemy not found in parents of {gameObject.name}");
    }

    

    private void HandleDeath()
    {
        gameObject.SetActive(false);
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (max > 0) ? Mathf.Clamp01((float)current / (float)max) : 0f;
        }
    }
    
    private void OnEnable()
    {
        if (enemy != null)
        {
            Debug.Log("Eventing");
            enemy.OnHPChanged += HandleHealthChanged;
            enemy.OnDied += HandleDeath;
            
            HandleHealthChanged(enemy.HP,enemy.MaxHP);
        }
    }
    private void OnDisable()
    {
        if (enemy != null)
        {
            enemy.OnHPChanged -= HandleHealthChanged;
            enemy.OnDied -=  HandleDeath;
        }
    }
}
