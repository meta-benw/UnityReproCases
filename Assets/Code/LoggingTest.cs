using System;
using System.Collections;
using UnityEngine;

public class LoggingTest : MonoBehaviour
{
    [NonSerialized] public GameObject NullRefObj = null;
    IEnumerator Start()
    {
        yield return null;
        Debug.LogWarning("Example Unity Debug.LogWarning");
        yield return null;
        Debug.LogError("Example Unity Debug.LogError");
        yield return null;
        Debug.Log("Upcoming null reference error is a \"safe\" example of LogsSection displaying Unity errors");
        NullRefObj.SetActive(false);
    }
}
