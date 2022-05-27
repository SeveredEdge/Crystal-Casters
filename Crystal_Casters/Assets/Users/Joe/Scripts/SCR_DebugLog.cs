using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_DebugLog : MonoBehaviour
{
    public static SCR_DebugLog Instance { get; private set; }

    private TextMeshProUGUI log;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Application.logMessageReceived += OutputLog;
            log = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }

    public void Refresh()
    {
        log.text = "Debug Log: ";
    }

    public IEnumerator DelayedClearLog()
    {
        yield return new WaitForSecondsRealtime(1f);
        Refresh();
    }

    private void OutputLog(string logString, string stackTrace, LogType type)
    {
        if (logString.Length == 0)
        {
            log.text += "\n" + stackTrace;
        }
        else
        {
            log.text += "\n" + logString;
        }
    }
}
