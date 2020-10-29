using System;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public abstract class Skill
{
    public string SkillName { get; protected set; }
    public int Cd { get; protected set; }
    public int LeftCd { get; protected set; }
    public SkillConfig Config { get; protected set; }
    protected const string ConfigPath = "Configs/Skills/";
    public Skill(SkillConfig config)
    {
        Config = config;
        SkillName = Config.SkillName;
        Cd = Config.Cd;
        LeftCd = Cd;
    }
    
    public void RefreshCd()
    {
        LeftCd = Math.Max(0, LeftCd - 1);
    }

    public abstract bool OnCastBegin(CharacterInfo caster);
    public abstract SkillResult OnCast(CharacterInfo caster, CharacterInfo target, Random rand);
}