using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Copy : MonoBehaviour
{
    public int number;
    public int copyNum;
    public SphereColor myColor;
    [SerializeField] private GameObject[] RedObj;
    WaitForSeconds time = new WaitForSeconds(0.1f);
    public TextMeshProUGUI text;
    public void TextResize(int size)//글자크기 강조 
    {
        StartCoroutine(COR_TextResize(size));
    }
    IEnumerator COR_TextResize(int size)
    {
        text.fontSize = size;
        yield return time;
        text.fontSize = 36;
    }
    public void SetRedObj(bool bl)// 색이 다른지역 위험 표시
    {
        for(int i=0;i<RedObj.Length;i++)
        {
            RedObj[i].SetActive(bl);
        }
    }
}
