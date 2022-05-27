using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CrystalCasting : MonoBehaviour
{
    private Mivry mivrySCR;
    private string currentCrystal = "";

    private void Awake()
    {
        mivrySCR = GetComponent<Mivry>();
        StartCoroutine(SCR_DebugLog.Instance.DelayedClearLog());
    }


    public void GestureCompleted(GestureCompletionData data)
    {
        //Debug.Log("Spell Attempted: " + data.gestureName + " + Similarity: " + data.similarity);
        //if (data.similarity <= 0.25f || !GetComponent<OVRGrabbable>().isGrabbed) return;
        if (!GetComponent<OVRGrabbable>().isGrabbed) return;

        if (data.gestureName == currentCrystal)
        {
            Debug.Log("Casting " + data.gestureName + " spell!");
        }
        else if (currentCrystal == "empty")
        {
            Debug.Log("Crystal doesn't have enough power!");
        }
        else
        {
            Debug.Log("Incorrect incantation or crystal!");
        }
    }

    public void AssignGestureControllers()
    {
        if (mivrySCR == null) return;

        mivrySCR.LeftHand = GameObject.FindGameObjectWithTag("Left_Hand");
        mivrySCR.RightHand = GameObject.FindGameObjectWithTag("Right_Hand");

        mivrySCR.enabled = false;
    }

    public string CurrentCrystal
    { 
        set 
        {
            switch (value)
            {
                case "WaterCrystal":
                    currentCrystal = "water";
                    break;
                case "FireCrystal":
                    currentCrystal = "fire";
                    break;
                case "EarthCrystal":
                    currentCrystal = "earth";
                    break;
                case "EmptyCrystal":
                    currentCrystal = "empty";
                    break;
                default:
                    currentCrystal = "";
                    break;
            }
        }
    }

    public Mivry Mivry
    {
        get { return mivrySCR; }
    }
}
