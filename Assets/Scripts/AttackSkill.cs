using System;
using Message;
using Unity.Mathematics;
using Random = System.Random;

public class AttackSkill : Skill 
{
    public float AttackFactor { get; private set; }
    public AttackSkill(SkillConfig config) : base(config)
    {
        AttackFactor = Config.AttackFactor;
    }

    public override bool OnCastBegin(CharacterInfo caster)
    {
        Messenger.Broadcast(MsgConst.ACTOR_ATTACK, caster.ID);
        return false;
    }

    public override SkillResult OnCast(CharacterInfo caster, CharacterInfo target, Random rand)
    {
        bool isMiss = rand.Next(100) < target.Dodge;
        bool isCritical = rand.Next(100) < caster.Critical;
        float atk = caster.Attack * AttackFactor / 100;
        float def = target.Denfence;
        int damage =  (int)math.ceil(atk * atk / (atk + def));
        if (isCritical)
            damage = (int)math.ceil(damage * caster.CriticalDamage / 100.0f);
        return new SkillResult(damage, isMiss, isCritical, null);
    }
}