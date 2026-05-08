using UnityEngine;
using UnityEngine.Rendering;

public class LogFFRAndMSAA : MonoBehaviour
{
    void Update()
    {
        if (Time.frameCount % 90 == 0)
        {
            var renderPipelineAsset = GraphicsSettings.currentRenderPipeline as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
            Debug.LogFormat("FFR level is {0}, MSAA count is {1}", OVRManager.foveatedRenderingLevel, renderPipelineAsset.msaaSampleCount);
        }
    }
}
