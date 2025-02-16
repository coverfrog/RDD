using System;

[Serializable]
public class UserData
{
    public readonly bool IsDebug;
    
    public string userName;
    public int i;

    public UserData(bool isDebug)
    {
        IsDebug = isDebug;
    }
}
