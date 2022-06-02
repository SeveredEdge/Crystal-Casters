using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WaterInteraction : MonoBehaviour
{
    private ParticleSystem magic;
    [SerializeField] private LayerMask doorLayer;

    private void Awake()
    {
        magic = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (magic.isPlaying)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position + (Vector3.down * 0.25f), transform.forward, out raycastHit, 3, doorLayer, QueryTriggerInteraction.Ignore))
            {
                Destroy(raycastHit.transform.gameObject);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (Vector3.down * 0.25f), transform.forward * 3);
    }*/
}
