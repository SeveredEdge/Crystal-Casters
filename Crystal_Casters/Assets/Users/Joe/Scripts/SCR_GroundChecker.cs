using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class SCR_GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Transform groundChecker;
    [SerializeField] private Transform respawnPos;
    
    private bool standingOnLog = false;
    private float logSpeed = 0;

    private void Awake()
    {
        groundChecker = transform;
    }
    private void FixedUpdate()
    {
        /*RaycastHit raycastHit;
        if (Physics.Raycast(groundChecker.position, Vector3.down, out raycastHit, 5, layerMask, QueryTriggerInteraction.Collide))
        {
            switch (raycastHit.transform.tag)
            {
                case "Log":
                    if (logSpeed == 0)
                    {
                        logSpeed = raycastHit.transform.GetComponent<SCR_LogFloat>().speed;
                        Debug.Log("Standing on Log!");
                    }
                    standingOnLog = true;
                    break;
                case "End_Trigger":
                    groundChecker.position = respawnPos.position;
                    break;
                default:
                    standingOnLog = false;
                    break;
            }
        }*/

        Collider[] colliders = Physics.OverlapSphere(groundChecker.position, 0.2f, layerMask, QueryTriggerInteraction.Collide);
        foreach(Collider c in colliders)
        {
            switch (c.transform.tag)
            {
                case "Log":
                    if (logSpeed == 0)
                    {
                        logSpeed = c.transform.GetComponent<SCR_LogFloat>().speed;
                        Debug.Log("Standing on Log!");
                    }
                    standingOnLog = true;
                    break;
                case "End_Trigger":
                    MixedRealityPlayspace.Transform.position = respawnPos.position;
                    break;
                default:
                    standingOnLog = false;
                    break;
            }
        }

        if (standingOnLog)
        {
            MixedRealityPlayspace.Transform.position += Vector3.left * logSpeed * Time.fixedDeltaTime;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down);
    }*/

}
