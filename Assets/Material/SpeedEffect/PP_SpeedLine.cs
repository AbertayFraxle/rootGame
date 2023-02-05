using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PP_SpeedLine : MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material,0);
    }
}
