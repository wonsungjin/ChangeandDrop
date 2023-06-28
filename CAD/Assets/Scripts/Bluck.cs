using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bluck : MonoBehaviour
{
    [SerializeField] private GameObject[] bluck;
    [SerializeField] private GameObject bluckBox;
    [SerializeField] private int num = 10;
    public TextMeshProUGUI text;
    public Transform target;
    public int broken;
    public int interval;
    public int count;
    public int reSize;
    bool isBroken;
    List<GameObject> list = new List<GameObject>();
    Coroutine co_Invoke;

    void Start()
    {
        target = gameObject.transform;
    }
    public void TextResize()//
    {
        text.text = (num- reSize).ToString();
    }

    private void OnTriggerEnter(Collider other)//블럭에 공들이 닿을때마다 각도가 점점휘어집니다.
    {
        if (other.CompareTag("Sphere")&&!list.Contains(other.gameObject))
        {
            list.Add(other.gameObject);
            other.GetComponent<Sphere>().stop = true;
            reSize++;
            broken ++;
            TextResize();
            if (interval<=broken)
            {
                broken = 0;
                count++;
                bluck[0].transform.Rotate(0, 0, 3.2f);
                bluck[1].transform.Rotate(0, 0, 1.2f);
                bluck[3].transform.Rotate(0, 0, -1.2f);
                bluck[4].transform.Rotate(0, 0, -3.2f);
                bluck[2].transform.Translate(0, -0.15f, 0);
                bluck[3].transform.Translate(0, -0.12f, 0);
                bluck[1].transform.Translate(0, -0.12f, 0);
            }
                if (num<= interval*count) Broken();
            if (GameMGR.Instance.sphereList.Count < num)
            {
                if (co_Invoke != null) StopCoroutine(co_Invoke);
                co_Invoke = StartCoroutine(Invoke_End());
            }
        }
    }
    IEnumerator Invoke_End()//공의 갯수가 모자를때 3초후에 리스타드 가능
    {
        yield return new WaitForSeconds(3f);
        GameMGR.Instance.ReStartUI();
    }
    public void Broken()//벽돌이 부서지는 이펙트
    {
        if (isBroken) return;
        for(int i=0;i<list.Count;i++) list[i].GetComponent<Sphere>().stop = false;
        isBroken = true;
        for (int i = 0; i<bluck.Length;i++)
        {
            Instantiate(bluckBox, bluck[i].transform.position+new Vector3(0,0,2),Quaternion.Euler(new Vector3(0,0,40)));
            Instantiate(bluckBox, bluck[i].transform.position+new Vector3(0,0,1),Quaternion.Euler(new Vector3(0, 0, 15)));
            Instantiate(bluckBox, bluck[i].transform.position+new Vector3(0,0,0),Quaternion.identity);
            Instantiate(bluckBox, bluck[i].transform.position+new Vector3(0,0,-1),Quaternion.Euler(new Vector3(0, 0, -15)));
            Instantiate(bluckBox, bluck[i].transform.position+new Vector3(0,0,-2),Quaternion.Euler(new Vector3(0, 0, -40)));
        }
        gameObject.SetActive(false);
    }
}
