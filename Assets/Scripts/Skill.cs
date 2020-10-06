using System;
using Random = System.Random;

public abstract class Skill
{
    public int Cd { get; protected set; }
    public int LeftCd { get; protected set; }

    public Skill()
    {
        Cd = 0;
        LeftCd = Cd;
    }
    
    public void RefreshCd()
    {
        LeftCd = Math.Max(0, LeftCd - 1);
    }

    public abstract SkillResult OnCast(CharacterInfo casterInfo, CharacterInfo targetInfo, Random rand);
}