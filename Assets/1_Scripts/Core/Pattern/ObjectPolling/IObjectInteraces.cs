using UnityEngine;
using UnityEngine.Pool;

namespace Cf.Pattern
{
    public interface IObjectPoolAble<T> where T : Behaviour
    {
        void SetPool(ObjectPool<T> pool);
    }
}
