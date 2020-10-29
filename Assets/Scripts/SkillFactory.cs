using System;

public class SkillFactory
{
    public static Skill CreateSkill(SkillConfig skillConfig)
    {
        return (Skill) Activator.CreateInstance(Type.GetType(skillConfig.ClassName), new Object[]{skillConfig});
    }
}