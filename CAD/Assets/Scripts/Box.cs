using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxA
{
    open,
    close,
    trun,
    move,
}
public class Box : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject right;//상자 오른쪽날개
    [SerializeField] private GameObject left;//상자 왼쪽날개
    [SerializeField] private GameObject[] dumiSphere;

    private float boxRotateX = 30;//상자 각도
    private float boxRotateZ = 180;//상자 회전각도
    private bool isClose;//상자 닫는중인지 체크하는 불값
    private bool isMove;//상자 이동 방향 체인지
    private bool isTrun;

    WaitForSeconds time = new WaitForSeconds(0.02f);
    Vector3 closeVettor = new Vector3(-3f, 0, 0);
    Vector3 openVettor = new Vector3(3f, 0, 0);
    Vector3 rightVettor = new Vector3(0, 0, -1f);
    Vector3 leftVettor = new Vector3(0, 0, 1f);
    Coroutine move;
    private void Start()
    {
        StartCoroutine(COR_BoxAction(BoxA.close));
    }
    
    public void BoxAction(BoxA boxAction)
    {
        StartCoroutine(COR_BoxAction(boxAction));
    }
    IEnumerator COR_BoxAction(BoxA boxAction)
    {
        if (boxAction == BoxA.close && !isClose) isClose = true;// 닫는 도중에 상자 뒤집지 못하게 방지
        else if (boxAction != BoxA.close && isClose) yield break;
        else if (boxAction == BoxA.trun)
        {
            StopCoroutine(move);
            if (isTrun == true) yield break;
            else isTrun = true;
        }
        while (true)
        {
            if (boxAction == BoxA.move)
            {
                yield return time;
                if (transform.position.x < -8.1f && !isMove) isMove = true;
                else if (transform.position.x > -0.5f && isMove) isMove = false;
                if (isMove) transform.Translate(0.2f, 0, 0);
                else transform.Translate(-0.2f, 0, 0);
                yield return null;
            }
            else if (boxAction == BoxA.trun)
            {
                if (boxRotateZ > 0 && transform.position.x <= -4.5f)
                {
                    boxRotateZ -= 1;
                    transform.Rotate(rightVettor);
                }
                else if (boxRotateZ > 0 && transform.position.x > -4.5f)
                {
                    boxRotateZ -= 1;
                    transform.Rotate(leftVettor);
                }
                else if (boxRotateZ <= 0)
                {
                    StartCoroutine(COR_BoxAction(BoxA.open));
                    break;
                }
                else break;
                yield return null;
            }
            else
            {
                yield return time;
                if (boxRotateX > -90 && boxAction == BoxA.close)
                {
                    right.transform.Rotate(closeVettor);
                    left.transform.Rotate(closeVettor);
                    boxRotateX -= 3f;
                }
                else if (boxRotateX < 10 && boxAction == BoxA.open)
                {
                    right.transform.Rotate(openVettor);
                    left.transform.Rotate(openVettor);
                    boxRotateX += 3f;
                }
                else if (boxRotateX <= -90 && boxAction == BoxA.close && isClose)//상자가 다닫혔을때
                {
                    isClose = false;
                    Debug.Log("닫힘");
                    for (int i = 0; i < dumiSphere.Length; i++) dumiSphere[i].SetActive(false);
                    move = StartCoroutine(COR_BoxAction(BoxA.move));

                }
                else if (boxRotateX >= 10 && boxAction == BoxA.open)//상자가 다열렸을때
                {
                    Debug.Log("열림");
                    GameMGR.Instance.Create_Sphere(6);
                    break;
                }
                else
                {
                    break;
                }
                yield return null;
            }
        }
    }


}
