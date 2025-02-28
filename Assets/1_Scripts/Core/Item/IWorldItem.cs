using System;
using UnityEngine;

namespace Cf.Item
{
    public interface IItem
    {
        public ulong GetItemIndex { get; }

        public void SetItemIndex(ulong itemIndex);
    }

    public interface IWorldItem : IItem
    {
        public void SetItemIndex(ulong itemIndex, object o)
        {
            SetItemIndex(itemIndex);
        }
    }
}