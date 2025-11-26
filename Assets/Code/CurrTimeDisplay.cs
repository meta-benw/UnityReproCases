using System;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class CurrTimeDisplay : MonoBehaviour
{
    public string TimestampFormat;
    TMPro.TMP_Text m_assocText;

    void Awake()
    {
        m_assocText = GetComponent<TMPro.TMP_Text>();
    }

    void Update()
    {
        m_assocText.text = DateTime.Now.ToString(TimestampFormat);
    }
}
