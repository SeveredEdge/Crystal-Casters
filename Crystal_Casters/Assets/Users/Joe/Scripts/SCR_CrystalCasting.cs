using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CrystalCasting : MonoBehaviour
{
    private Mivry mivrySCR;

    private void Awake()
    {
        mivrySCR = GetComponent<Mivry>();
    }

    public void GestureCompleted(GestureCompletionData data)
    {
        if (data.similarity >= 0.25f && data.similarity <= 1)
        {
            Debug.Log(data.gestureName);
            Debug.Log(data.similarity);
        }
    }

    public void AssignGestureControllers()
    {
        if (mivrySCR == null) return;

        mivrySCR.LeftHand = GameObject.FindGameObjectWithTag("Left_Hand");
        mivrySCR.RightHand = GameObject.FindGameObjectWithTag("Right_Hand");
    }
}
