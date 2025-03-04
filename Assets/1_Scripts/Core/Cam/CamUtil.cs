using UnityEngine;
using UnityEngine.AI;

namespace Cf.Cams
{
    public static class CamUtil 
    {
        public static bool ToRay(Vector3 screenPoint, LayerMask layerMask, out int hitCount, out RaycastHit[] result, float distance = 1000.0f)
        {
            var cam = CamTrigger.Get(CamType.Main);
            
            if (!cam)
            {
                hitCount = 0;
                result = null;
                return false;
            }

            var ray = cam.ScreenPointToRay(screenPoint);
            result = new RaycastHit[10];

            hitCount = Physics.RaycastNonAlloc(ray, result, distance, layerMask);
            
            return hitCount > 0;
        }

        public static RaycastHit GetNearHit(int hitCount, RaycastHit[] result)
        {
            RaycastHit nearHit = result[0];
            
            for (int idx = 1; idx < hitCount; idx++)
            {
                if (result[idx].distance < nearHit.distance)
                {
                    nearHit = result[idx];
                }
            }

            return nearHit;
        }
    }
}
