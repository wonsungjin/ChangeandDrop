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
    [SerializeField] public GameObject right;//상자 오른쪽날개
    [SerializeField] public GameObject left;//상자 왼쪽날개
    private float boxRotateX = 30;//상자 각도
    private bool isClose;//상자 닫는중인지 체크하는 불값
    private bool isMove;//상자 이동 방향 체인지

    WaitForSeconds time = new WaitForSeconds(0.02f);
    Vector3 closeVettor = new Vector3(-3f, 0, 0);
    Vector3 openVettor = new Vector3(3f, 0, 0);
    IEnumerator COR_BoxAction(BoxAction boxAction)
    {
        if (boxAction == BoxAction.close && !isClose) isClose = true;// 닫는 도중에 상자 뒤집지 못하게 방지
        else if (boxAction != BoxAction.close && isClose) yield break;
            Debug.Log("실행");
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
                    Debug.Log("닫힘");
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
