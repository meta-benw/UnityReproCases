using UnityEngine;
using UnityEngine.Rendering;

public class SetMSAAButton : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.MsaaQuality MsaaCount;
    public void SetMsaaLevel()
    {
        var renderPipelineAsset = GraphicsSettings.currentRenderPipeline as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
        renderPipelineAsset.msaaSampleCount = (int)MsaaCount;
    }
}
