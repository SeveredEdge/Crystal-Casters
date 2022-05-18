using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Teleport;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;


public class SCR_Teleportation : MonoBehaviour
{
    private OVRGrabbable crystal;
    private List<TeleportScripts> teleportScripts = new List<TeleportScripts>();

    private void Start()
    {
        crystal = GetComponent<OVRGrabbable>();
        crystal.grabEvent.AddListener(TeleportEnable);
        crystal.dropEvent.AddListener(TeleportDisable);
    }

    private struct TeleportScripts
    {
        public ParabolicTeleportPointer pointer;
        public ParabolaLineDataProvider dataProvider;
        public MixedRealityLineRenderer lineRenderer;
        public AudioSource audio;
        public IMixedRealityTeleportHotspot previousHotspot;
        public TeleportScripts(ParabolicTeleportPointer p)
        {
            pointer = p;
            dataProvider = p.GetComponent<ParabolaLineDataProvider>();
            lineRenderer = p.GetComponent<MixedRealityLineRenderer>();
            audio = p.GetComponent<AudioSource>();
            previousHotspot = p.TeleportHotspot;
        }

        public void SetActive(bool value)
        {
            dataProvider.enabled = value;
            lineRenderer.enabled = value;
            audio.enabled = value;
            pointer.hasCrystal = value;
            if (value)
                pointer.TeleportHotspot = previousHotspot;
            else
                pointer.TeleportHotspot = new TeleportHotspot();
        }
    }

    private void LateUpdate()
    {
        if (teleportScripts.Count == 0)
        { 
            ParabolicTeleportPointer[] lst = FindObjectsOfType<ParabolicTeleportPointer>();
            for (int i = 0; i < lst.Length; i++)
            {
                SCR_DebugLog.Instance.Print("Controller " + i.ToString() + " connected");
                teleportScripts.Add(new TeleportScripts(lst[i]));
                teleportScripts[i].SetActive(false);
            }
        }
        //Ensures no other mrtk componenet re-enables teleporting
        else
        {
            foreach (TeleportScripts t in teleportScripts)
            {
                if (t.lineRenderer.isActiveAndEnabled && !crystal.isGrabbed)
                {
                    t.SetActive(false);
                }
            }
        }
    }


    private void TeleportEnable()
    {
        for (int i = 0; i < teleportScripts.Count; i++)
        {
            if (crystal.grabbedBy.GetComponent<MixedRealityControllerVisualizer>().Handedness == teleportScripts[i].pointer.Handedness)
            {
                teleportScripts[i].SetActive(true);
            }
            else
            {
                teleportScripts[i].SetActive(false);
            }
        }
    }

    private void TeleportDisable()
    {
        for (int i = 0; i < teleportScripts.Count; i++)
        {
            teleportScripts[i].SetActive(false);
        }
    }
}
