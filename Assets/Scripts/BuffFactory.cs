using System;

public class BuffFactory
{
    public static Buff CreateBuff(BuffConfig buffConfig)
    {
        return (Buff) Activator.CreateInstance(Type.GetType(buffConfig.ClassName), new Object[]{buffConfig});
    }
}