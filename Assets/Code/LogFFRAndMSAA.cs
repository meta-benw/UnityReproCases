using UnityEngine;
using UnityEngine.Rendering;

public class LogFFRAndMSAA : MonoBehaviour
{
    void Update()
    {
        if (Time.frameCount % 90 == 0)
        {
            var renderPipelineAsset = GraphicsSettings.currentRenderPipeline as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
            Debug.LogFormat("FFR level is {0} dynamic FFR is {1}, MSAA count is {2}", OVRManager.foveatedRenderingLevel, OVRManager.useDynamicFoveatedRendering, renderPipelineAsset.msaaSampleCount);
        }
    }
}
