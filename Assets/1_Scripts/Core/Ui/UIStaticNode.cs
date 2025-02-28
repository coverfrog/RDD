using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Ui
{
    public abstract class UIStaticNode<TEnum> : MonoBehaviour where TEnum : Enum
    {
        [Header("L0 : Option")]
        [SerializeField] private TEnum mTEnum;

        [Header("L0 : Reference")] 
        [SerializeField] private UIStaticNode<TEnum> mParentNode;
        [SerializeField] private List<UIStaticNode<TEnum>> mChildNodeList;
        
        private bool _mIsTypeInit;
        private bool _mIsRootNode;

        #region :: Get

        public TEnum GetEnumType() => mTEnum;

        public bool GetIsRootNode() => _mIsRootNode;
        
        #endregion

        #region :: Unity

        private void Awake()
        {
            Init();
        }

        #endregion

        #region :: Init

        private void Init()
        {
            if (_mIsTypeInit)
            {
                return;
            }

            if (mTEnum == null)
            {
                return;
            }

            if (mParentNode)
            {
                _mIsRootNode = false;
            }

            _mIsTypeInit = true;
        }

        #endregion
    }
}
