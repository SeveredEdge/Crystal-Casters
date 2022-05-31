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


    private void OutputLog(string logString, string stackTrace, LogType type)
    {
        if (log.text.Length >= 300)
        {
            Refresh();
        }

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
