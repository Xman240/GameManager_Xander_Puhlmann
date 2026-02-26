using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AIMovement))]
public abstract class Enemy : MonoBehaviour,IDamageable
{
    public int enemyID;
    [Header("Combat Params")] 
    public int MaxHP;
    public int HP;
    public int ATK;
    public int DEF;
    public float attackDelay;

    [Header("Behavior Ranges")]
    public CircleOverlap sightline;
    public CircleOverlap attackRange;

    protected Vector2 playerPosition;

    public Vector2 PlayerPosition => playerPosition;

    public event Action<bool> OnAttackingChanged;
    public event Action<int,int> OnHPChanged;

    private Coroutine attackCoroutine;

    public Vector2 patrolRange;
    private Vector2 startingPosition;
    private Vector2 nextPosition;
    private AIMovement aiMovement;
    
    
    private bool patroling;
    
    private bool isDead = false;
    public event Action OnDied;

    public abstract void Attack();
    public abstract void Die();

    private void Awake()
    {
        startingPosition = transform.position;
        sightline.OnOverlap += SetPlayerPosition;
        attackRange.OnOverlap += SetPlayerPosition;
        aiMovement = GetComponent<AIMovement>();
        aiMovement.OnArrive += Patrol;
    }

    private void Update()
    {
        if (isDead) return;
        
        if (attackRange.CircleOverlapCheck())
        {
            aiMovement.StopMovement();
            StartAttackCoroutine();
            return;
        }

        if (sightline.CircleOverlapCheck())
        {
            aiMovement.StopMovement();
            Pursue();

            return;
        }

        if (!patroling)
        {
            Patrol();
            patroling = true;
        }
        
    }
    public void UpdateHP(int hp, int maxHP)
    {
        MaxHP = maxHP;
        HP = Mathf.Clamp(hp, 0, MaxHP);
        OnHPChanged?.Invoke(HP,MaxHP);
    }

    public void SetPlayerPosition(Vector2 pos_)
    {
        playerPosition = pos_;
    }
    public void Patrol()
    {
        nextPosition = new Vector2(Random.Range(startingPosition.x - patrolRange.x, startingPosition.x + patrolRange.x),
            Random.Range(startingPosition.y - patrolRange.y, startingPosition.y + patrolRange.y));
        aiMovement.InitializeMovement(nextPosition);
    }

    public void TakeDamage(int dmg_)
    {
        if (dmg_ <= 0) return;
        if(HP <= 0) return;
        
        HP -= dmg_;    
        
        if(HP <= 0) HP = 0;
        
        OnHPChanged?.Invoke(HP,MaxHP);
        
        if (HP == 0)
        {
            HandleDeath();
        }
    }
    public void Pursue()
    {
        aiMovement.InitializeMovement(playerPosition);
    }
    public void StartAttackCoroutine()
    {
        if (attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        OnAttackingChanged?.Invoke(true);
        
        while (attackRange.CircleOverlapCheck())
        {
            Attack();
            yield return new WaitForSeconds(attackDelay);
        }
        OnAttackingChanged?.Invoke(false);
        attackCoroutine = null;
    }

    private void HandleDeath()
    {
        if (isDead) return;
        isDead = true;
        
        if(aiMovement != null)
        {
            aiMovement.StopMovement();
        }

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = null;

        OnDied?.Invoke();


    }


}
