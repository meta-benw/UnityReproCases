using UnityEngine;

public class SetFFRButton : MonoBehaviour
{
    public OVRPlugin.FoveatedRenderingLevel FFRLevel;
    public void SetFFRLevel()
    {
        OVRPlugin.foveatedRenderingLevel = FFRLevel;   
    }
}
