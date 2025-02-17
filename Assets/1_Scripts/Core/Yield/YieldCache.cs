using System.Collections.Generic;
using UnityEngine;

namespace Cf.Yield
{
    
    public static class YieldCache
    {
        // * tip
        // only use data -> null, because not alloc
        // require wait gl update -> end of frame | other, because wait gl update
    
        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

        private static readonly Dictionary<float, WaitForSeconds> TimeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            if (!TimeInterval.TryGetValue(seconds, out WaitForSeconds wfs))
            {
                TimeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
            }

            return wfs;
        }
    }

    internal class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals (float x, float y)
        {
            return Mathf.Approximately(x, y);
        }
        int IEqualityComparer<float>.GetHashCode (float obj)
        {
            return obj.GetHashCode();
        }
    }
}