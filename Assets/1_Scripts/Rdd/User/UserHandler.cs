using Cf.Pattern;
using UnityEngine;

public class UserManager : GenericSingleton<UserManager>
{
    public UserData LocalUserData { get; set; }
}
