using System;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class AttackSkill : Skill 
{
    public float AttackFactor { get; private set; }
    public SkillConfig config;
    public AttackSkill(String name)
    {
        config = Resources.Load<SkillConfig>(name);
        LeftCd = Cd = config.Cd;
        AttackFactor = config.AttackFactor;
    }
    
    public override SkillResult OnCast(CharacterInfo casterInfo, CharacterInfo targetInfo, Random rand)
    {
        bool isMiss = rand.Next(100) < targetInfo.Dodge;
        bool isCritical = rand.Next(100) < casterInfo.Critical;
        float atk = casterInfo.Attack * AttackFactor / 100;
        float def = targetInfo.Denfence;
        int damage =  (int)math.ceil(atk * atk / (atk + def));
        if (isCritical)
            damage = damage * casterInfo.CriticalDamage / 100;
        return new SkillResult(damage, isMiss, isCritical, null);
    }
}