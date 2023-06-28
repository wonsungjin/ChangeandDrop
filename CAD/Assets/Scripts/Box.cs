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
    [SerializeField] private GameObject right;//���� �����ʳ���
    [SerializeField] private GameObject left;//���� ���ʳ���
    [SerializeField] private GameObject[] dumiSphere;

    private float boxRotateX = 30;//���� ����
    private float boxRotateZ = 180;//���� ȸ������
    private bool isClose;//���� �ݴ������� üũ�ϴ� �Ұ�
    private bool isMove;//���� �̵� ���� ü����
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
        if (boxAction == BoxA.close && !isClose) isClose = true;// �ݴ� ���߿� ���� ������ ���ϰ� ����
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
                else if (boxRotateX <= -90 && boxAction == BoxA.close && isClose)//���ڰ� �ٴ�������
                {
                    isClose = false;
                    Debug.Log("����");
                    for (int i = 0; i < dumiSphere.Length; i++) dumiSphere[i].SetActive(false);
                    move = StartCoroutine(COR_BoxAction(BoxA.move));

                }
                else if (boxRotateX >= 10 && boxAction == BoxA.open)//���ڰ� �ٿ�������
                {
                    Debug.Log("����");
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
