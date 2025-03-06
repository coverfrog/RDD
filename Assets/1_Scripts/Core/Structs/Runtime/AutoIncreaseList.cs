using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cf.Structs
{
    [Serializable]
    public class AutoIncreaseList<T> : IEnumerable<T> where T : struct
    {
        [SerializeField] private List<T> list = new List<T>();

        public static implicit operator List<T>(AutoIncreaseList<T> tList)
        {
            if (tList != null)
            {
                if (tList.list != null)
                {
                    return tList.list;
                }

                return tList.list = new List<T>();
            }

            tList = new AutoIncreaseList<T>
            {
                list = new List<T>()
            };

            return tList.list;
        }

        private void AutoIncrease(int idx)
        {
            if (idx < list.Count)
            {
                return;
            }

            while (idx >= list.Count)
            {
                list.Add(default);
            }
        }

        public T this[int idx]
        {
            get
            {
                AutoIncrease(idx);
                return list[idx];
            }
            set
            {
                AutoIncrease(idx);
                list[idx] = value;
            }
        }

        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}