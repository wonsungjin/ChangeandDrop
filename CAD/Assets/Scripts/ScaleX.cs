using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleX : MonoBehaviour
{
    WaitForSeconds time = new WaitForSeconds(0.1f);

    void Start()//네모 터지는 이펙트
    {
        StartCoroutine(COR_Broken_Time());
        GetComponent<Rigidbody>().velocity = new Vector2(0, 10);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-10f,10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }


IEnumerator COR_Broken_Time()// 부서진 잔해 크기 축소
{
    for (float i = 1.1f; i > 0; i -= 0.04f)
    {
        yield return time;
        transform.localScale = new Vector3(i, i, i);
    }
    yield return null;
        Destroy(gameObject);
}
}
