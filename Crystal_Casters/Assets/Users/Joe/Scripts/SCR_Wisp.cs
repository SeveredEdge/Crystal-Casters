using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Wisp : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public GameObject correctCrystal;
    private bool crystalInRange = false;

    private Coroutine coroutine;
    private LineRenderer lineRenderer;

    [SerializeField] private Material[] materials;
    [SerializeField] private string crystalTag;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {

        Collider[] playerItems = Physics.OverlapSphere(transform.position, 0.4f, layerMask, QueryTriggerInteraction.Ignore);
        SCR_CrystalState emptyCrystal = null;

        foreach(Collider obj in playerItems)
        {
            if (obj.CompareTag("EmptyCrystal"))
            {
                emptyCrystal = obj.GetComponent<SCR_CrystalState>();
                break;
            }
        }
        crystalInRange = (emptyCrystal != null);

        if (!crystalInRange)
        {
            lineRenderer.enabled = false;
            return;
        }

        //Correct crystal is decided once 1 wisp is claimed
        if (correctCrystal != null)
        {
            if (emptyCrystal.gameObject != correctCrystal) return;
        }


        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.GetChild(0).position);
        lineRenderer.SetPosition(1, emptyCrystal.transform.position);


        if (coroutine == null)
        {
            coroutine = StartCoroutine(ChargeCrystal(emptyCrystal.gameObject));
        }
    }

    private IEnumerator ChargeCrystal(GameObject crystal)
    {
        WaitForSecondsRealtime time = new WaitForSecondsRealtime(1f);
        for (int i = 0; i < 5; i++)
        {
            yield return time;
            if (!crystalInRange)
            {
                coroutine = null;
                yield break;
            }
        }

        FillCrystal(crystal);
    }

    private void FillCrystal(GameObject crystal)
    {
        foreach (SCR_Wisp wisp in FindObjectsOfType<SCR_Wisp>())
        {
            wisp.correctCrystal = crystal;
        }
        //Advance Material and Destroy this obj

        SCR_CrystalState crystalState = crystal.GetComponent<SCR_CrystalState>();
        crystalState.chargeState++;

        crystal.transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[crystalState.chargeState - 1];
        if (crystalState.chargeState == 3)
        {
            crystal.tag = crystalTag;
            crystalState.enabled = false;
            FindObjectOfType<SCR_CrystalCasting>().CurrentCrystal = crystalTag;
        }

        Destroy(gameObject);
    }
}
