using UnityEngine;

public class SetDynamicFFRButton : MonoBehaviour
{
    public bool IsDynamic;
    public void SetDynamicFFR()
    {
        Debug.Log("Setting Dynamic FFR to " + IsDynamic);
        OVRManager.useDynamicFoveatedRendering = IsDynamic;   
    }
}
