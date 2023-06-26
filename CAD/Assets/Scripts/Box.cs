using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxAction
{
    open,
    close,
    trun,
    move,
}
public class Box : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] public GameObject right;//���� �����ʳ���
    [SerializeField] public GameObject left;//���� ���ʳ���
    private float boxRotateX = 30;//���� ����
    private bool isClose;//���� �ݴ������� üũ�ϴ� �Ұ�
    private bool isMove;//���� �̵� ���� ü����

    WaitForSeconds time = new WaitForSeconds(0.02f);
    Vector3 closeVettor = new Vector3(-3f, 0, 0);
    Vector3 openVettor = new Vector3(3f, 0, 0);
    IEnumerator COR_BoxAction(BoxAction boxAction)
    {
        if (boxAction == BoxAction.close && !isClose) isClose = true;// �ݴ� ���߿� ���� ������ ���ϰ� ����
        else if (boxAction != BoxAction.close && isClose) yield break;
            Debug.Log("����");
        while (true)
        {
            if (boxAction == BoxAction.move)
            {
                yield return time;
                if (transform.position.x < -8.1f && !isMove) isMove = true;
                else if (transform.position.x > -0.5f && isMove) isMove = false;
                if(isMove) transform.Translate(0.2f, 0, 0);
                else transform.Translate(-0.2f, 0, 0);
                yield return null;
            }
            else if (boxAction == BoxAction.trun)
            {

            }
            else
            {
                yield return time;
                if (boxRotateX > -90 && boxAction == BoxAction.close)
                {
                    right.transform.Rotate(closeVettor);
                    left.transform.Rotate(closeVettor);
                    boxRotateX -= 3f;
                }
                else if (boxRotateX < 10 && boxAction == BoxAction.open)
                {
                    right.transform.Rotate(openVettor);
                    left.transform.Rotate(openVettor);
                    boxRotateX += 3f;
                }
                else if(boxRotateX <= -90 && boxAction == BoxAction.close&&isClose)
                {
                    isClose = false;
                    Debug.Log("����");
                    StartCoroutine(COR_BoxAction(BoxAction.move));

                }
                else 
                {
                    break;
                }
                yield return null;
            }
        }
    }
    private void Start()
    {
        StartCoroutine(COR_BoxAction(BoxAction.close));
    }

}
