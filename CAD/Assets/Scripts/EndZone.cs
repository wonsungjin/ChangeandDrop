using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
        
    private void OnTriggerEnter(Collider other)
    {
        if (GameMGR.Instance.end == false) GameMGR.Instance.end = true;
        if (other.CompareTag("Sphere"))
        {
            other.GetComponent<SphereCollider>().radius = 0.3f;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            GameMGR.Instance.Clear();
        }
    }

}
