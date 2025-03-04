using UnityEngine;

namespace Cf.Cams
{
    public static class CamUtil 
    {
        public static bool ToRay()
        {
            if (!CamTrigger.Get(CamType.Main))
            {
                return false;
            }

            return true;
        }
    }
}
