public class BattleLogHelper
{
    private static string GenCharacter(Actor baseActor)
    {
        string color = baseActor.IsPlayer ? "blue" : "yellow";
        return $"<color=\"{color}\">{baseActor.CharacterInfo.Name}</color>";
    }
    
    private static string GenDamage(SkillResult skillResult)
    {
        if (skillResult.IsMiss)
            return "Miss";
        string critical = skillResult.IsCritical ? "Critical! " : "";
        return $"{critical}cause <color=\"red\">{skillResult.Damage}</color> damage.";
    }
        
    public static string GenerateBattleLog(Actor caster, Actor target, Skill skill, SkillResult skillResult)
    {
        return $"<color=\"blue\">{GenCharacter(caster)}</color> {skill.SkillName} > <color=\"yellow\">{GenCharacter(target)}</color>, {GenDamage(skillResult)}\n";
    } 
}