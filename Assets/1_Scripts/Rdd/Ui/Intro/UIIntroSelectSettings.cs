using UnityEngine;

public class UIIntroSelectSettings : UIIntroSelect
{
    public override UIIntroSelectType GetSelectType()
    {
        return UIIntroSelectType.Settings;
    }
    
    public override void OnInteract(UIIntroSelector introSelector)
    {
        introSelector.OnClickEventComplete();
    }
}
