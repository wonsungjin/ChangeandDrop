using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    private Rigidbody rb;
    public int copyNum = 0;
    public Vector3 velocity;
    public bool stop=false;
    public SphereColor color = SphereColor.blue;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = rb.velocity;
        velocity.x = 0;
        velocity.y = -10;
        rb.velocity = velocity;
    }
    private void FixedUpdate()
    {
        if (transform.position.y < GameMGR.Instance.GetMinPos()) GameMGR.Instance.SetMinPos(gameObject);
        velocity = rb.velocity;
        if (velocity.y < -10) velocity.y += 1;
        else if (velocity.y > 4) velocity.y -= 1;
        if (velocity.x < -4) velocity.x = -4;
        else if (velocity.x > 4) velocity.x = 4;
        rb.velocity = velocity;
    }
    private void OnTriggerEnter(Collider other)//색이 다른곳에 닿을시 오브젝트풀에 다시 넣고 리스트에서 제거.
    {
        if (other.CompareTag("BlueZone") && GameMGR.Instance.sphereColor != SphereColor.blue) 
        { 
            GameMGR.Instance.AddSphere(this);
            GameMGR.Instance.objectPool.DestroyPrefab(this);
        }
        else if (other.CompareTag("YellowZone") && GameMGR.Instance.sphereColor != SphereColor.yellow)
        {
            GameMGR.Instance.AddSphere(this);
            GameMGR.Instance.objectPool.DestroyPrefab(this); 
        }
        if (GameMGR.Instance.sphereList.Count == 0) GameMGR.Instance.ReStartUI();
    }
    private void OnTriggerExit(Collider other)//존 밖으로 나갈시 해당 존의 곱하기갯수만큼 복제
    {
        if(other.CompareTag("BlueZone") &&other.GetComponent<Copy>().number>=copyNum)
        {
            if(color == SphereColor.blue)
            {
                copyNum++;
                GameMGR.Instance.Create_Sphere(other.GetComponent<Copy>().copyNum,copyNum,gameObject);
                other.GetComponent<Copy>().TextResize(40);
            }
        }
        if (other.CompareTag("YellowZone") && other.GetComponent<Copy>().number >= copyNum)
        {
            if (color == SphereColor.yellow)
            {
                copyNum++;
                GameMGR.Instance.Create_Sphere(other.GetComponent<Copy>().copyNum, copyNum, gameObject);
                other.GetComponent<Copy>().TextResize(40);

            }
        }
    }

}
