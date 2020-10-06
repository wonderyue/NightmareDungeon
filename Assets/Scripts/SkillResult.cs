using System.Collections.Generic;
using UnityEngine;

public class SkillResult
{
    public int Damage { get; private set; }
    public bool IsMiss { get; private set; }
    public bool IsCritical { get; private set; }
    public List<Buff> Buffs { get; private set; }

    public SkillResult(int damage, bool isMiss, bool isCritical, List<Buff> buffs)
    {
        Damage = damage;
        IsMiss = isMiss;
        IsCritical = isCritical;
        Buffs = buffs;
    }
}