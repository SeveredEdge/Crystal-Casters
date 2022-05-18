using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlaceItem : MonoBehaviour
{
    [Header("Object to place into")]
    [SerializeField] private Transform containerObj;
    [Header("Distance before snap placement")]
    [SerializeField] private float snapDistance;
    [Header("Distance to move before snap is enabled")]
    [SerializeField] private float enableDistance;


    private Transform item;
    private bool isHeld = false, justGrabbed = true, snapReady = false;
    private OVRGrabbable grabbable;
    private Rigidbody rb;
    [SerializeField]private Animation anim;
    [SerializeField] private Outline outline;

    private void Awake()
    {
        item = transform;
        grabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();
        
        outline.enabled = false;
        outline.transform.SetParent(containerObj);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.rotation = Quaternion.identity;

        grabbable.grabEvent.AddListener(ItemHeld);
        grabbable.dropEvent.AddListener(ItemDropped);
    }

    private void Update()
    {
        //Only check for snap when item is held
        if (!isHeld) return;

        //Checks whether the item is newly picked up
        if (justGrabbed)
        {
            //Once the item has left the predetermined snap range, it can now be snapped back
            if (Vector3.Distance(containerObj.position, item.position) > enableDistance)
            {
                justGrabbed = false;
            }
            else
            {
                return;
            }
        }


        if (Vector3.Distance(containerObj.position, item.position) <= snapDistance)
        {
            snapReady = true;
            if (!outline.isActiveAndEnabled) outline.enabled = true;
            if (!anim.isPlaying) anim.Play();
        }
        else
        {
            snapReady = false;
            if (outline.isActiveAndEnabled) outline.enabled = false;
            if (anim.isPlaying) anim.Stop();
        }
    }

    private void SnapToPosition()
    {
        item.SetParent(containerObj);
        item.localPosition = outline.transform.localPosition;
        item.rotation = outline.transform.rotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }

    private void ItemHeld()
    {
        isHeld = true;
        justGrabbed = true;
    }

    private void ItemDropped()
    {
        isHeld = false;
        outline.enabled = false;
        rb.isKinematic = false;
        if (anim.isPlaying) anim.Stop();
        if (snapReady)
        {
            SnapToPosition();
        }
    }
}
