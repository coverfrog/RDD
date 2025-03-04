using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Cams
{
    public enum CamType
    {
        Main,
        Ui,
    }

    [RequireComponent(typeof(Camera))]
    public class CamTrigger : MonoBehaviour
    {
        private static readonly Dictionary<CamType, CamTrigger> CamTriggerDict = new Dictionary<CamType, CamTrigger>();

        public static Camera Get(CamType camType)
        {
            if (CamTriggerDict.TryGetValue(camType, out var camTrigger))
            {
                return camTrigger.Cam;
            }

            return null;
        }

        [SerializeField] private CamType mCamType;

        private Camera Cam { get; set; }
        
        private void Awake()
        {
            Cam = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            if (CamTriggerDict.TryGetValue(mCamType, out var prevCam))
            {
                prevCam.OnUnSub();

                CamTriggerDict[mCamType] = this;
                
            }

            else
            {
                CamTriggerDict.Add(mCamType, this);
            }
        }

        private void OnUnSub()
        {
            gameObject.SetActive(false);
        }
    }
}
