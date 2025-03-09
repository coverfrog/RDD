using UnityEngine;

public class UIIntroSelectQuit : UIIntroSelect
{
    public override UIIntroSelectType GetSelectType()
    {
        return UIIntroSelectType.Quit;
    }
    
    public override void OnInteract(UIIntroSelector introSelector)
    {
        
    }
}
