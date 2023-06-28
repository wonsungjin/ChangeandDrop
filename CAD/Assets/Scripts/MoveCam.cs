using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    Vector3 pos = Vector3.zero;
    float smoothSpeed = 0.05f;//���� ���ǵ�
    public Transform target;
    float speed= -85f;//��������� ������ ī�޶� �̵����ǵ�
    private void LateUpdate()
    {
        if(GameMGR.Instance.end)
        {
            if(speed >-100) speed -= 0.1f;
            Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(transform.position.x, speed, transform.position.z), smoothSpeed);;
            transform.position = smoothPos;
        }
        else
        if (target != null)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position,new Vector3(transform.position.x,target.position.y,transform.position.z), smoothSpeed);
            transform.position = smoothPos;
        }
    }
}
