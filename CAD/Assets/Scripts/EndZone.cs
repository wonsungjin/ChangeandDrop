using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
        
    private void OnTriggerEnter(Collider other)//끝날시 공들의 라디어스를 줄여 상자로 빠르게 들어가게 함
    {
        if (GameMGR.Instance.end == false) GameMGR.Instance.end = true;
        if (other.CompareTag("Sphere"))
        {
            other.GetComponent<SphereCollider>().radius = 0.3f;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }
    private void OnTriggerExit(Collider other)//상자에 들어가는 갯수체크
    {
        if (other.CompareTag("Sphere"))
        {
            GameMGR.Instance.Clear();
        }
    }

}
