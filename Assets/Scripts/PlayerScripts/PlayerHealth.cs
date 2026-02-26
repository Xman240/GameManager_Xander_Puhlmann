using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image HealthBar;
    
    public event Action OnDeath;
    
    
    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        if(currentHealth <= 0) return;
        
        currentHealth -= damage;    
        
        if(currentHealth <= 0) currentHealth = 0;
        
        UpdateHealthBar();

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }

    [ContextMenu("Heal Full")]
    public void HealFull()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        HealthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
