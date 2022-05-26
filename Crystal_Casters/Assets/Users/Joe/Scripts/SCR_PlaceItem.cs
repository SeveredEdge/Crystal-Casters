using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class SCR_PlaceItem : MonoBehaviour
{
    [Header("Objects to place into")]
    [SerializeField] private Transform[] containerObjects;
    [Header("Belt slot to place into")]
    [SerializeField] private Transform beltObj;
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
    private Transform lastObject, desiredObject;


    private void Awake()
    {
        item = transform;
        grabbable = GetComponent<OVRGrabbable>();
        rb = GetComponent<Rigidbody>();
        
        outline.enabled = false;
        lastObject = beltObj;
        MoveOutline(beltObj);

        grabbable.grabEvent.AddListener(ItemHeld);
        grabbable.dropEvent.AddListener(ItemDropped);
    }

    private void MoveOutline(Transform newPos)
    {
        if (outline.transform.parent == newPos) return;

        outline.transform.SetParent(newPos);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        //Only check for snap when item is held
        if (!isHeld) return;

        //Checks whether the item is newly picked up
        if (justGrabbed)
        {
            //Once the item has left the predetermined snap range, it can now be snapped back
            if (Vector3.Distance(lastObject.position, item.position) > enableDistance)
            {
                justGrabbed = false;
            }
            else
            {
                return;
            }
        }

        Transform closestObj = containerObjects[0];
        for (int i = 1; i < containerObjects.Length; i++)
        {
            float distance = Vector3.Distance(containerObjects[i].position, item.position);
            if (distance < Vector3.Distance(closestObj.position, item.position))
            {
                closestObj = containerObjects[i];
            }
        }

        if (Vector3.Distance(closestObj.position, item.position) <= snapDistance)
        {
            snapReady = true;
            MoveOutline(closestObj);
            desiredObject = closestObj;
            if (!outline.isActiveAndEnabled) outline.enabled = true;
            //if (!anim.isPlaying) anim.Play();
        }
        else
        {
            snapReady = false;
            if (outline.isActiveAndEnabled) outline.enabled = false;
            //if (anim.isPlaying) anim.Stop();
        }
    }

    private void SnapToPosition()
    {
        //Debug.Log("Snapped to: " + desiredObject.name);

        item.SetParent(desiredObject);
        lastObject = desiredObject;
        item.localPosition = outline.transform.localPosition;
        item.rotation = outline.transform.rotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        //Debug.Log("Item parent = " + item.parent);
        //Debug.Log("Container object's parent: " + desiredObject.parent.name);
    }

    public void ItemHeld()
    {
        isHeld = true;
        justGrabbed = true;
        //Debug.Log("Item is Held!");
    }

    public void ItemDropped()
    {
        isHeld = false;
        outline.enabled = false;
        rb.isKinematic = false;
        //if (anim.isPlaying) anim.Stop();
        if (snapReady)
        {
            SnapToPosition();
        }
        else
        {
            item.SetParent(MixedRealityPlayspace.Transform);
        }
    }
}
