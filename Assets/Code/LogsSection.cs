using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

public class LogsSection : MonoBehaviour
{
    public enum eType
    {
        Callback,
        Log,
        Warning,
        Error,
    }

    struct LogData
    {
        public DateTime Timestamp;
        public eType LogType;
        public string Message;
        public string Details;
    }

    public string TimestampFormat;
    public LogLine LogLinePrefab;
    public RectTransform LogLinesParent;
    public Color CallbackColor, LogColor, WarningColor, ErrorColor;

    List<LogData> m_newLogs = new List<LogData>();
    StringBuilder m_logSB = new StringBuilder();

    public bool ShowCallbacks { get; private set; } = true;
    public bool ShowLogs { get; private set; } = true;
    public bool ShowWarnings { get; private set; } = true;
    public bool ShowErrors { get; private set; } = true;

    void Awake()
    {
        for (int i = LogLinesParent.childCount - 1; i >= 0; --i)
            GameObject.Destroy(LogLinesParent.GetChild(i).gameObject);
        Application.logMessageReceivedThreaded += LogMessageReceivedCallback;
    }

    //this is a default MonoBehaviour function, https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationFocus.html
    void OnApplicationFocus(bool hasFocus)
    {
        LogCallback("MonoBehaviour OnApplicationFocus: " + (hasFocus ? "true" : "false"));
    }

    //this is a default MonoBehaviour function, https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html
    void OnApplicationPause(bool pauseStatus)
    {
        LogCallback("MonoBehaviour OnApplicationPause: " + (pauseStatus ? "true" : "false"));
    }

    void LogMessageReceivedCallback(string logString, string stackTrace, LogType type)
    {
        LogData val = new LogData();
        val.Timestamp = DateTime.Now;
        val.LogType = UnityLogTypeToThisLogType(type);
        val.Message = logString;
        val.Details = stackTrace;
        m_newLogs.Add(val);
    }

    void LogCallback(string logString)
    {
        try
        {
            LogData val = new LogData();
            val.Timestamp = DateTime.Now;
            val.LogType = eType.Callback;
            val.Message = logString;
            string method, fileName, fileLineNumber;
            try
            {
                StackTrace st = new StackTrace(true);
                StackFrame sf = st.GetFrame(2);
                method = sf.GetMethod().ToString();
                fileName = sf.GetFileName();
                fileLineNumber = sf.GetFileLineNumber().ToString();
            }
            catch (Exception)
            {
                method = "[Unknown]";
                fileName = "[Unknown]";
                fileLineNumber = "[Unknown]";
            }
            val.Details = String.Format("{0} {1} Line: {2}", method, fileName, fileLineNumber);
            m_newLogs.Add(val);
        }
        catch (Exception e)
        {
            m_newLogs.Add(new LogData
            {
                Timestamp = DateTime.Now,
                LogType = eType.Callback,
                Message = "[Failure during LogCallback : " + e.Message + "]",
                Details = e.StackTrace
            });
        }
    }

    public void ClearAllLogs()
    {
        m_newLogs.Clear();
        for (int i = LogLinesParent.childCount - 1; i >= 0; --i)
        {
            Destroy(LogLinesParent.GetChild(i).gameObject);
        }
    }

    public void Mark()
    {
        m_newLogs.Add(new LogData
        {
            Timestamp = DateTime.Now,
            LogType = eType.Callback,
            Message = "MARK",
            Details = ""
        });
    }

    eType UnityLogTypeToThisLogType(LogType logType)
    {
        switch (logType)
        {
            case LogType.Exception:
            case LogType.Assert:
            case LogType.Error:
                return eType.Error;
            case LogType.Warning:
                return eType.Warning;
            case LogType.Log:
                return eType.Log;
            default: throw new System.Exception();
        }
    }

    Color TypeToColor(eType logType)
    {
        switch (logType)
        {
            case eType.Error: return ErrorColor;
            case eType.Warning: return WarningColor;
            case eType.Log: return LogColor;
            case eType.Callback: return CallbackColor;
            default: throw new System.Exception();
        }
    }

    void Update()
    {
        foreach (var newLog in m_newLogs)
        {
            bool shouldShow = true;
            switch (newLog.LogType)
            {
                case eType.Callback: shouldShow = ShowCallbacks; break;
                case eType.Error: shouldShow = ShowErrors; break;
                case eType.Warning: shouldShow = ShowWarnings; break;
                case eType.Log: shouldShow = ShowLogs; break;
            }
            if (!shouldShow) continue;
            
            m_logSB.Clear();
            var text = GameObject.Instantiate(LogLinePrefab, LogLinesParent);

            m_logSB.Append(String.Format("{0,12}", newLog.Timestamp.ToString(TimestampFormat)));
            string logType = newLog.LogType.ToString().ToUpper();
            m_logSB.Append(String.Format("{0,5}", logType.Substring(0, Math.Min(4, logType.Length))));
            m_logSB.Append(' ');
            m_logSB.Append(newLog.Message);

            text.Set(m_logSB.ToString(), newLog.Details, TypeToColor(newLog.LogType));
        }
        m_newLogs.Clear();
    }
}
