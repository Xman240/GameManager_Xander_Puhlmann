using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public CharacterStats stats;

}
[Serializable]
public class CharacterStats
{
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public int ATK;
    public int DEF;
    public int MATK;
    public int MDEF;
    public int AGI;
}
