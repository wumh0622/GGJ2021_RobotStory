using UnityEngine;

public class LightingRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Camera lightingCamera = null;
    [SerializeField]
    private Material screenMaterial = null;
    [SerializeField]
    private Material cloneMaterial = null;

    private RenderTexture originalTemp;
    private RenderTexture lightingTemp;

    private void Awake()
    {
        Debug.Assert(mainCamera != null, this);
        Debug.Assert(lightingCamera != null, this);
        Debug.Assert(screenMaterial != null, this);

        int antiAliasing = QualitySettings.antiAliasing;
        if (antiAliasing == 0)
        {
            antiAliasing = 1;
        }

        cloneMaterial = new Material(screenMaterial);
        cloneMaterial.SetTexture("_MainTex", originalTemp);
        cloneMaterial.SetTexture("_RenderTexture", lightingTemp);

        lightingCamera.gameObject.SetActive(true);
    }

    private void OnPreRender()
    {
        if (originalTemp != null)
        {
            mainCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(originalTemp);
            originalTemp = null;
        }

        if (lightingTemp != null)
        {
            lightingCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(lightingTemp);
            lightingTemp = null;
        }

        int antiAliasing = QualitySettings.antiAliasing;
        if (antiAliasing == 0)
        {
            antiAliasing = 1;
        }

        originalTemp = RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, antiAliasing);
        mainCamera.targetTexture = originalTemp;

        lightingTemp = RenderTexture.GetTemporary(lightingCamera.pixelWidth, lightingCamera.pixelHeight, 24);
        lightingCamera.targetTexture = lightingTemp;

        cloneMaterial.SetTexture("_MainTex", originalTemp);
        cloneMaterial.SetTexture("_RenderTexture", lightingTemp);
    }

    private void OnPostRender()
    {
        mainCamera.targetTexture = null;
        Graphics.Blit(originalTemp, null, cloneMaterial);
    }

    private void OnApplicationQuit()
    {
        if (originalTemp != null)
        {
            mainCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(originalTemp);
            originalTemp = null;
        }

        if (lightingTemp != null)
        {
            lightingCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(lightingTemp);
            lightingTemp = null;
        }
    }

    private void OnDestroy()
    {
        if (originalTemp != null)
        {
            mainCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(originalTemp);
            originalTemp = null;
        }

        if (lightingTemp != null)
        {
            lightingCamera.targetTexture = null;
            RenderTexture.ReleaseTemporary(lightingTemp);
            lightingTemp = null;
        }
    }
}
