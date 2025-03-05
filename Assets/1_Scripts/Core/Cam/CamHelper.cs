using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Cams
{
    [RequireComponent(typeof(Camera))]
    public class CamHelper : MonoBehaviour
    {
        [Header("Option")] 
        [SerializeField] private CamType mCamType;
        
        [Header("Reference")] 
        [SerializeField] private Camera mCamera;

        #region :: Get

        public CamType GetCamType() => mCamType;

        public Camera GetCamera() => mCamera;

        #endregion

        #region :: Unity

        private void Awake()
        {
            if (!mCamera) TryGetComponent(out mCamera);
        }

        private void OnEnable()
        {
            CamUtil.Set(this);    
        }

        #endregion

        #region :: On Action

        public void OnSet()
        {
            
        }

        public void OnUnSet()
        {
            
        }

        #endregion
    }
}
