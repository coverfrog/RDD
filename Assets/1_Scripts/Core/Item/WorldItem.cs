using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Item
{
    public class WorldItem : MonoBehaviour, IWorldItem
    {
        [Header("L0 : Reference")]
        [SerializeField] protected Collider mCol;

        [Header("L0 : View")] 
        [SerializeField] private ulong mItemIndex;

        #region :: Unity

        private void Start()
        {
            SetItemIndex(123);
            SetItemIndex(123, null);
        }

        #endregion
        
        #region :: IItemWorld

        public ulong GetItemIndex => mItemIndex;
        
        public void SetItemIndex(ulong itemIndex)
        {
            Debug.Log("v1");
        }
        
        public void SetItemIndex(ulong itemIndex, object o)
        {
            
        }
       

        #endregion

    }
}
