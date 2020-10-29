using System;

public abstract class Buff
{
    
    public string BuffName { get; protected set; }
    public int Cd { get; protected set; }
    public int LeftCd { get; protected set; }
    public BuffConfig.BuffType Type { get; protected set; }
    public BuffConfig Config { get; protected set; }
    public bool IsActive { get; protected set; }
    protected const string ConfigPath = "Configs/Skills/";
    public Buff(BuffConfig config)
    {
        Config = config;
        BuffName = Config.BuffName;
        Type = Config.Type;
        Cd = Config.Cd;
        LeftCd = Cd;
        IsActive = true;
    }
    
    public void RefreshCd()
    {
        LeftCd = Math.Max(0, LeftCd - 1);
    }
    
    public abstract bool OnBuffAdded(CharacterInfo caster);
    public abstract bool OnBuffRemoved(CharacterInfo caster);

    public virtual void OnRoundBegin(CharacterInfo caster)
    {
        LeftCd = Cd;
        if (Type == BuffConfig.BuffType.Once)
        {
            IsActive = false;
        }
    } 
}