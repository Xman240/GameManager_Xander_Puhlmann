using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(SpriteAnimator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AIAnimator : MonoBehaviour
{
    AIMovement aiMovement;
    SpriteAnimator spriteAnimation;
    SpriteRenderer spriteRenderer;
    private Enemy enemy;
    public List<AnimationStateData> animations;
    private Dictionary<PlayerAnimationState, AnimationData> animationDictionary;
    public PlayerAnimationState currentState;
    
    private bool isAttacking;
    private bool isDead;
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteAnimation = GetComponent<SpriteAnimator>();
        enemy = GetComponent<Enemy>();
        aiMovement.OnVelocityChange += UpdateAnimation;
        enemy.OnAttackingChanged += HandleAttackingChanged;
        enemy.OnDied += HandleDeath;
        InitializeAnimationDictionary();
    }
    

    private void InitializeAnimationDictionary()
    {
        animationDictionary = new Dictionary<PlayerAnimationState, AnimationData>();
        foreach (AnimationStateData animationStateData in animations)
        {
            animationDictionary.Add(animationStateData.state, animationStateData.animation);
        }
    }

    private void UpdateAnimation(Vector2 moveDir_)
    {
        if (isAttacking) return;
        if(isDead) return;
        
       float absX = Mathf.Abs(moveDir_.x);
       float absY = Mathf.Abs(moveDir_.y);
        PlayerAnimationState prevState = currentState;
        
        if(absX > absY)
        {
            if (moveDir_.x > 0)
            {
                currentState = PlayerAnimationState.WALK_RIGHT;
                spriteRenderer.flipX = true;
            }
            else
            {
                currentState = PlayerAnimationState.WALK_LEFT;
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (moveDir_.y > 0)
            {
                currentState = PlayerAnimationState.WALK_UP;
            }
            else
            {
                currentState = PlayerAnimationState.WALK_DOWN;
            }
        }

        if(currentState != prevState) spriteAnimation.InitializeAnimation(animationDictionary[currentState]);

    }

    private void HandleAttackingChanged(bool attacking)
    {
        isAttacking  = attacking;

        if (isAttacking)
        {
            float xdirection = enemy.PlayerPosition.x - transform.position.x;
            
            if(xdirection > 0) spriteRenderer.flipX = true;
            else if(xdirection < 0) spriteRenderer.flipX = false;

           SetAnimationState(PlayerAnimationState.ATTACK);
        }
    }

    private void HandleDeath()
    {
        isDead = true;
        SetAnimationState(PlayerAnimationState.DEATH);
    }
    
    private void SetAnimationState(PlayerAnimationState newState)
    {
        currentState = newState;

        if (animationDictionary.TryGetValue(newState, out AnimationData animationData))
        {
            spriteAnimation.InitializeAnimation(animationData);
        }
        else
        {
            Debug.Log("Cant find animatioon");
        }
    }
    
    private void OnDestroy()
    {
        if (aiMovement != null) aiMovement.OnVelocityChange -= UpdateAnimation;
        if (enemy != null)
        {
            enemy.OnAttackingChanged -= HandleAttackingChanged;
            enemy.OnDied -= HandleDeath;
        }
    }
    
}
