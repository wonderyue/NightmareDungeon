using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/SkillConfig", order = 1)]
public class SkillConfig : ScriptableObject
{
    public string SkillName;
    public int Cd;
    public int AttackFactor;
    public string ClassName;
}