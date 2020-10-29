using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/BuffConfig", order = 1)]
public class BuffConfig : ScriptableObject
{
    public enum BuffType
    {
        Once,
        Forever,
    }
    public string BuffName;
    public int Cd;
    public BuffType Type;
    public Sprite Icon;
    public int Value;
    public string ClassName;
}