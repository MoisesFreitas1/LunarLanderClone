using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float alpha;
    public float omega;

    private void FixedUpdate()
    {
        //gameObject.transform.Translate(transform.position + new Vector3(alpha * Mathf.Sin(omega * Time.deltaTime), alpha * Mathf.Cos(omega * Time.deltaTime), 0));
    }
}
