using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPostProcessShaderToCamera : MonoBehaviour
{
    [SerializeField] private Shader ppShader;
    [SerializeField] private Color mainColor;
    [SerializeField, Range(50, 500)] private float radius;
    [SerializeField, Range(0f, 0.2f)] private float smoothEdge;
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCam;

    private Material screenMaterial;
    public  Material ScreenMaterial
    {
        get
        {
            if (screenMaterial == null)
            {
                screenMaterial = new Material(ppShader);
                screenMaterial.hideFlags = HideFlags.HideAndDontSave;
            }

            return screenMaterial;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        Debug.Log("hit");
        if (ppShader != null)
        {
            Debug.Log("hit2");
            ScreenMaterial.SetColor("_Color", mainColor);
            ScreenMaterial.SetVector("_Position", mainCam.WorldToScreenPoint(target.position));
            //probably dont wanna do this every frame
            ChangeCircle(radius, smoothEdge);
            Graphics.Blit(sourceTexture, destTexture, ScreenMaterial);
        }
        else
        {
            Debug.Log("hitelse");
            Graphics.Blit(sourceTexture, destTexture);
        }
    }


    void Start()
    {
        if (!ppShader && !ppShader.isSupported)
        {
            enabled = false;
        }
        else
        {
            ChangeResolution();
        }

        mainCam = Camera.main;
    }


    public void ChangeResolution() 
    {
        ScreenMaterial.SetFloat("_ScreenHeight", Screen.height);
        ScreenMaterial.SetFloat("_ScreenWidth", Screen.width);
    }

    public void ChangeCircle(float rad, float smooth) 
    {
        ScreenMaterial.SetFloat("_Radius", rad);
        ScreenMaterial.SetFloat("_Smoothness", smooth);
    }

    private void OnDisable()
    {
        if (screenMaterial)
        {
            DestroyImmediate(screenMaterial);
        }
    }
}
