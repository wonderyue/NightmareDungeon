using System;
using System.Collections.Generic;

[Serializable]
public class CharacterInfo
{
    public int ID;
    public string Name;
    public int RawAttack;
    public int RawDenfence;
    public int RawDodge;
    public int RawCritical;
    public int RawCriticalDamage;
    public int RawHp;
    public int RawSpeed;
    public int Attack;
    public int Denfence;
    public int Dodge;
    public int Critical;
    public int CriticalDamage;
    public int Hp;
    public int Speed;
    public int Exp;
    public int Level;
    public List<Buff> Buffs;
    public List<Skill> ActiveSkills;
    public CharacterInfo(CharacterConfig characterConfig)
    {
        ID = characterConfig.ID;
        Name = characterConfig.Name;
        RawAttack = Attack = characterConfig.Attack;
        RawDenfence = Denfence = characterConfig.Denfence;
        RawDodge = Dodge = characterConfig.Dodge;
        RawCritical = Critical = characterConfig.Critical;
        RawCriticalDamage = CriticalDamage = characterConfig.CriticalDamage;
        RawHp = Hp = characterConfig.Hp;
        RawSpeed = Speed = characterConfig.Speed;
        Exp = characterConfig.Exp;
        Level = 1;
        ActiveSkills = new List<Skill>();
        foreach (var skillConfig in characterConfig.skills)
        {
            ActiveSkills.Add(SkillFactory.CreateSkill(skillConfig));
        }
        Buffs = new List<Buff>();
        foreach (var buffConfig in characterConfig.buffs)
        {
            Buffs.Add(BuffFactory.CreateBuff(buffConfig));
        }
    }
}