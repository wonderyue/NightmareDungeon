using Unity.Mathematics;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [field: SerializeField] public int RawAttack { get; private set; }
    [field: SerializeField] public int RawDenfence { get; private set; }
    [field: SerializeField] public int RawDodge { get; private set; }
    [field: SerializeField] public int RawCritical { get; private set; }
    [field: SerializeField] public int RawCriticalDamage { get; private set; }
    [field: SerializeField] public int RawHp { get; private set; }
    [field: SerializeField] public int RawSpeed { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public int Denfence { get; private set; }
    [field: SerializeField] public int Dodge { get; private set; }
    [field: SerializeField] public int Critical { get; private set; }
    [field: SerializeField] public int CriticalDamage { get; private set; }
    [field: SerializeField] public int Hp { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
    
    public CharacterInfo(int attack, int denfence, int dodge, int critical, int criticalDamage, int hp, int exp, int level, int speed)
    {
        RawAttack = Attack = attack;
        RawDenfence = Denfence = denfence;
        RawDodge = Dodge = dodge;
        RawCritical = Critical = critical;
        RawCriticalDamage = CriticalDamage = criticalDamage;
        RawHp = Hp = hp;
        RawSpeed = Speed = speed;
        Exp = exp;
        Level = level;
    }

    public void AddHp(int value)
    {
        Hp = math.clamp(Hp + value, 0, RawHp);
        if (Hp == 0)
        {
            Debug.Log("die");
        }
        Debug.Log(Hp);
    }
}