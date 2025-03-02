using UnityEngine;
using UnityEngine.EventSystems;

namespace Cf.Ui
{
    public class UINode : MonoBehaviour
    {
        protected bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
