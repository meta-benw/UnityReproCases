using UnityEngine;

public class SetFFRButton : MonoBehaviour
{
    public OVRManager.FoveatedRenderingLevel FFRLevel;
    public void SetFFRLevel()
    {
        Debug.Log("Setting FFRLevel to " + FFRLevel);
        OVRManager.foveatedRenderingLevel = FFRLevel;   
    }
}
