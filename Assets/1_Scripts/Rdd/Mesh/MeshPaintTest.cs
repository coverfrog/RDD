using System;
using Cf.Cams;
using UnityEngine;

public class MeshPaintTest : MonoBehaviour
{
    [SerializeField] private int mBrushSize = 1;
    [SerializeField] private Texture2D mBrushTexture2D;
    [SerializeField] private Renderer mRend;

    private Texture2D _mRuntimeTex;
    
    private void Start()
    {
        CreatInstance();
        
        InputManager.Instance.OnLeftClick += InstanceOnOnLeftClick;
    }

    private void InstanceOnOnLeftClick(bool obj)
    {
        if (!obj) return;

        if (!CamUtil.ToRay(Input.mousePosition, 1 << 0, out var resultCount, out var hits)) return;

        var hit = CamUtil.GetNearHit(resultCount, hits);
      
        if (!hit.collider) return;

        if (!hit.collider.gameObject.TryGetComponent<Renderer>(out var rend)) return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        
        if (!tex) return;
        
        Vector2 pixelUV = hit.textureCoord;
        
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        
        PaintTexture(tex, (int)pixelUV.x, (int)pixelUV.y);
    }

    private void CreatInstance()
    {
        Texture2D tex = mRend.material.mainTexture as Texture2D;
        
        if (!tex) return;

        _mRuntimeTex = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, true);
        
        Graphics.CopyTexture(tex, _mRuntimeTex);
        
        _mRuntimeTex.Apply();

        mRend.material.mainTexture = _mRuntimeTex;
    }

    private void PaintTexture(Texture2D tex, int centerX, int centerY)
    {
        int brushWidth = mBrushTexture2D.width * mBrushSize;
        int brushHeight = mBrushTexture2D.height * mBrushSize;

        Color[] brushPixels = mBrushTexture2D.GetPixels();
    
        for (int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int texX = centerX + x - brushWidth / 2;
                int texY = centerY + y - brushHeight / 2;

                if (texX >= 0 && texX < tex.width && texY >= 0 && texY < tex.height)
                {
                    Color brushColor = brushPixels[y * brushWidth + x];
                    Color baseColor = tex.GetPixel(texX, texY);
                    Color blendedColor = Color.Lerp(baseColor, brushColor, brushColor.a);
                    
                    tex.SetPixel(texX, texY, blendedColor);
                }
            }
        }
        
        tex.Apply();
    }
    
}
