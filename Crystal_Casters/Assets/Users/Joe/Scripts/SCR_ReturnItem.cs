using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class SCR_ReturnItem : MonoBehaviour
{
    [SerializeField] private Transform parentObj;
    private LayerMask playerLayer;
    private Transform item;
    private Rigidbody rb;
    private OVRGrabbable grabbable;

    
    private void Awake()
    {
        item = transform;
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();
        playerLayer = LayerMask.NameToLayer("Player");

        ReturnToBelt();

        grabbable.grabEvent.AddListener(ItemHeld);
    }

    public void ItemHeld()
    {
        item.SetParent(grabbable.grabbedBy.transform);
        //Debug.Log(item.parent.name);
        rb.useGravity = true;
    }

    private void ReturnToBelt()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        item.SetParent(parentObj);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.Euler(Vector3.zero);
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != playerLayer)
        {
            Debug.Log("Returned to belt");
            //SCR_DebugLog.Instance.Print(LayerMask.LayerToName(other.gameObject.layer).ToString());
            ReturnToBelt();
        }
    }
}
