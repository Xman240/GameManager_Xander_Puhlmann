using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public CharacterStats stats;

    public void InitializeCharacterData(CharacterSO config_)
    {
        stats = new CharacterStats();
        stats = config_.stats;
        stats.currentHP = config_.stats.currentHP;
        stats.maxHP = config_.stats.maxHP;
    }
}
