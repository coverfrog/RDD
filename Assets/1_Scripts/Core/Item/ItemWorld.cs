using UnityEngine;

namespace Cf.Item
{
    public class ItemWorld : MonoBehaviour, IItemWorld
    {
        [Header("L0 : Reference")]
        [SerializeField] protected Collider mCol;

        [Header("L0 : View")] 
        [SerializeField] protected ulong mIndex;
        
        public Collider Col => mCol;

        public ulong GetItemIndex => mIndex;

        public void SetItem(ulong index)
        {
            mIndex = index;
        }

        public bool IsGetAvailable()
        {
            return true;
        }

        public void InteractItem()
        {
            
        }
    }
}
