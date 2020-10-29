using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig", order = 2)]
public class CharacterConfig : ScriptableObject
{
    public string Name;
    public int ID;
    public int Attack;
    public int Denfence;
    public int Dodge;
    public int Critical;
    public int CriticalDamage;
    public int Hp;
    public int Exp;
    public int Speed;
    public List<SkillConfig> skills;
    public List<BuffConfig> buffs;
}