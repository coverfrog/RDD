using System;
using UnityEngine;
using UnityEngine.AI;

namespace Cf.Cams
{
    public enum CamType
    {
        Main,
        Ui,
    }
    
    
    
    public static class CamUtil
    {
        #region :: Cam Reference

        private static CamHelper _mainCamHelper, _uiCamHelper;

        public static void Set(CamHelper camHelper)
        {
            var camType = camHelper.GetCamType();

            switch (camType)
            {
                case CamType.Main:
                {
                    if (_mainCamHelper) 
                    {
                        _mainCamHelper.OnUnSet();
                    }

                    _mainCamHelper = camHelper;
                }
                    break;
                case CamType.Ui:
                {
                    if (_uiCamHelper)
                    {
                        _uiCamHelper.OnUnSet();
                    }

                    _uiCamHelper = camHelper;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(camType), camType, null);
            }
            
            camHelper.OnSet();
        }

        public static void DebugLogCam()
        {
            string msg0 = _mainCamHelper != null ? _mainCamHelper.gameObject.name : "Null";
            string msg1 = _uiCamHelper != null ? _uiCamHelper.gameObject.name : "Null";
            
            Debug.Log("[Cam Util] Main : " + msg0);
            Debug.Log("[Cam Util] Ui : " + msg1);
        }

        #endregion

        #region :: Ray

        public static bool ToRay(Vector3 screenPoint, LayerMask layerMask, out int resultCount, out RaycastHit[] result, int hitCount = 20, float distance = 1000.0f)
        {
            if (!_mainCamHelper)
            {
                resultCount = 0;
                result = null;
                return false;
            }

            Ray ray = _mainCamHelper.GetCamera().ScreenPointToRay(screenPoint);
            
            result = new RaycastHit[hitCount];
            resultCount = Physics.RaycastNonAlloc(ray, result, distance, layerMask);

            return hitCount > 0;
        }

        public static RaycastHit GetNearHit(int resultCount, RaycastHit[] result)
        {
            RaycastHit nearHit = result[0];
            
            for (int idx = 1; idx < resultCount; idx++)
            {
                if (result[idx].distance < nearHit.distance)
                {
                    nearHit = result[idx];
                }
            }

            return nearHit;
        }
        
        #endregion
    }
}
