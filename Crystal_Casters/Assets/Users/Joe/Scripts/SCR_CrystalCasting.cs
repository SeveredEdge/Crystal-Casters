using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CrystalCasting : MonoBehaviour
{
    private Mivry mivrySCR;
    private string currentCrystal = "";

    [SerializeField]
    private ParticleSystem waterParticle, fireParticle, earthParticle;
    [HideInInspector] public ParticleSystem currentParticle = new ParticleSystem();


    private void Awake()
    {
        mivrySCR = GetComponent<Mivry>();
    }


    public void GestureCompleted(GestureCompletionData data)
    {
        //Debug.Log("Spell Attempted: " + data.gestureName + " + Similarity: " + data.similarity);
        //if (data.similarity <= 0.25f || !GetComponent<OVRGrabbable>().isGrabbed) return;
        if (!GetComponent<OVRGrabbable>().isGrabbed) return;

        if (data.gestureName == currentCrystal && data.similarity >= CrystalThreshold(data.gestureName) && data.similarity < 1)
        {
            Debug.Log("Casting " + data.gestureName + " spell!");
            Debug.Log("Similarity: " + data.similarity);
            PlayParticleEffect();
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

    private float CrystalThreshold(string crystal)
    {
        float threshold = 0;
        switch (crystal)
        {
            case "water":
                threshold = 0.3f;
                break;
            case "fire":
                threshold = 0.02f;
                break;
            case "earth":
                threshold = 0.3f;
                break;
        }
        return threshold;
    }

    private void PlayParticleEffect()
    {
        switch (currentCrystal)
        {
            case "water":
                if (waterParticle) { currentParticle = waterParticle; }
                break;
            case "fire":
                if (waterParticle) { currentParticle = fireParticle; }
                break;
            case "earth":
                if (waterParticle) { currentParticle = earthParticle; }
                break;
        }

        if (currentParticle.isStopped) { currentParticle.Play(); }
        mivrySCR.currentParticle = currentParticle;
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
