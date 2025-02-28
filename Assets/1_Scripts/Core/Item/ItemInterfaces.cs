using UnityEngine;

namespace Cf.Item
{
    public interface IItemWorld
    {
        public Collider Col { get; }
        
        public ulong GetItemIndex { get; }

        public void SetItem(ulong index);
        
        public bool IsGetAvailable();
        
        public void InteractItem();
    }

    public interface IItemData
    {
        public void Set(ulong index);

        public void Get();
    }
}