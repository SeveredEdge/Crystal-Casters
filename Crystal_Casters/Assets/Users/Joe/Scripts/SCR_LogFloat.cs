using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LogFloat : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    private Transform log;
    private Rigidbody rb;
    public float speed = 1f;
    private void Awake()
    {
        log = transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        log.position += Vector3.left * speed * Time.fixedDeltaTime;
        //Vector3 newPos = log.position + Vector3.left;
        //rb.MovePosition(newPos * 1f * Time.fixedDeltaTime);
        //rb.AddForce(Vector3.left * 1f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End_Trigger"))
        {
            log.position = new Vector3(startPos.position.x, log.position.y, log.position.z);
        }
    }
}
