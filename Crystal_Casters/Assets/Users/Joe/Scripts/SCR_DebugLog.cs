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
        }
    }

    public void Print<T>(T str)
    {
        if (log == null)
        {
            log = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        log.text += "\n" + str.ToString();
    }

    public void Refresh()
    {
        log.text = "Debug Log: ";
    }
}
