using Cf.Pattern;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    public Camera MainCam
    {
        get
        {
            if (_mMainCam != null) return _mMainCam;
            
            _mMainCam = Camera.main;

            if (_mMainCam != null) return _mMainCam;

            _mMainCam = Camera.current;
            
            if (_mMainCam != null) return _mMainCam;

            _mMainCam = new GameObject().AddComponent<Camera>();
            _mMainCam.transform.SetParent(transform);

            return _mMainCam;
        }
    }

    private Camera _mMainCam;
}
