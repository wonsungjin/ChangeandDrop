using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum SphereColor
{
    blue,
    yellow
}
public class GameMGR : MonoBehaviour
{
    #region singleton
    private static GameMGR instance;
    public static GameMGR Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMGR>();

                if (instance == null)
                {
                    GameObject gameMGR = new GameObject();
                    instance = gameMGR.AddComponent<GameMGR>();
                    gameMGR.name = "GameMGR";

                    DontDestroyOnLoad(gameMGR);
                }
            }

            return instance;
        }
    }
    #endregion
    [field: SerializeField] public float gameProgress { get; private set; } = 40;
    [SerializeField] private Material[] mat;
    [SerializeField] private Sphere sphere;
    public ObjectPool objectPool;
    public GameObject restart;
    public Box box;
    public MoveCam moveCam;

    public SphereColor sphereColor;

    private int count;
    private float min;
    public bool end;

    Copy[] copys;
    public List<Sphere> sphereList = new List<Sphere>();
    private void Awake()
    {
            copys = FindObjectsOfType<Copy>();
        sphereColor = SphereColor.blue;
        for (int i = 0; i < copys.Length; i++)
        {
            if (copys[i].myColor == SphereColor.yellow) copys[i].SetRedObj(true);
            else copys[i].SetRedObj(false);
        }
        min = gameProgress;
        box = FindObjectOfType<Box>();
        moveCam = FindObjectOfType < MoveCam>();
        objectPool = GetComponent<ObjectPool>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseClick();
        }
        if (restart.activeSelf == true&&Input.GetKeyDown(KeyCode.R))
        {
            ReStart();
        }
    }
    public void ReStartUI()//리스타트 ui 활성화
    {
         restart.SetActive(true); 
    }
    public void ReStart()//신을 재로드시켜 게임재시작
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MouseClick()// 마우스클릭 시 공색변경 및 게임시작
    {
        box.BoxAction(BoxA.trun);
        if (sphereList.Count == 0) return;
        if (sphereColor == SphereColor.blue)
        {
            sphereColor = SphereColor.yellow;
            for (int i = 0; i < sphereList.Count; i++)
            {
                ChangeColor(sphereList[i], SphereColor.yellow);
            }
            for (int i = 0; i < copys.Length; i++)
            {
                if(copys[i].myColor==SphereColor.blue) copys[i].SetRedObj(true);
                else copys[i].SetRedObj(false);
            }

            }
        else if (sphereColor == SphereColor.yellow)
        {
            sphereColor = SphereColor.blue;
            for (int i = 0; i < sphereList.Count; i++)
            {
                ChangeColor(sphereList[i], SphereColor.blue);
            }
            for (int i = 0; i < copys.Length; i++)
            {
                if (copys[i].myColor == SphereColor.yellow) copys[i].SetRedObj(true);
                else copys[i].SetRedObj(false);
            }
        }
    }
    public void ChangeColor(Sphere obj, SphereColor sc)// 공색 변경
    {
        if(sc == SphereColor.yellow)
        {
                obj.GetComponent<MeshRenderer>().material = mat[1];
                obj.GetComponent<TrailRenderer>().material = mat[3];
            obj.color = SphereColor.yellow;
        }
        else if (sc == SphereColor.blue)
        {
                obj.GetComponent<MeshRenderer>().material = mat[0];
                obj.GetComponent<TrailRenderer>().material = mat[2];
            obj.color = SphereColor.blue;
        }
    }
    public void AddSphere(Sphere obj)//공리스트에  추가 및 제거
    {
        if (sphereList.Contains(obj))
            sphereList.Remove(obj);
        else
            sphereList.Add(obj);
    }
    public float GetMinPos()// 젤 밑에있는공의 y값
    {
        return min;
    }
    public void SetMinPos(GameObject obj)// 공중에 제일 밑에있는 공을 카메라 타겟으로 삼음
    {
        moveCam.target = obj.transform;
        min = obj.transform.position.y;
        gameProgress = min;
    }
    public void Create_Sphere(int num, int copy=0, GameObject pos= null)//단일 복제와 여러개 복제 선택가능
    {
        if(pos != null)
        {
            Sphere obj = null;
            for (int i = 0; i < num-1; i++)
            {
                obj = GameMGR.instance.objectPool.CreatePrefab(sphere, pos.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f), Quaternion.identity);
                obj.copyNum = copy;
                GameMGR.Instance.AddSphere(obj);
            }
                return;
        }
        for (int i = 0; i < num; i++)
        {
            Invoke("Invoke_Create_Sphere", 0.05f * i);

        }
    }
    private void Invoke_Create_Sphere()//한번에 복제하면 리지드바디로 튕겨져 나가기때문에 일정시간후에 복제
    {
        count++;
        Sphere obj = null;
        if (count == 2) obj = GameMGR.instance.objectPool.CreatePrefab(sphere, box.gameObject.transform.position + new Vector3(0.5f, -1f, 0f), Quaternion.identity);
        else if (count == 3)
        {
            count -= 3;
            obj = GameMGR.instance.objectPool.CreatePrefab(sphere, box.gameObject.transform.position + new Vector3(-0.5f, -0.8f, 0f), Quaternion.identity);
        }
        else obj = GameMGR.instance.objectPool.CreatePrefab(sphere, box.gameObject.transform.position + new Vector3(0, -1.2f, 0f), Quaternion.identity);
        GameMGR.Instance.AddSphere(obj);
    }
    public TextMeshProUGUI clearText;
    public int clearNum=1000;
    bool clear;
    Coroutine co_Invoke;
    public void Clear()// 상자에 공이들어갈때마다 호출되는 함수
    {
        if (clearNum > 0)
        {
            clearNum--;
            clearText.text = clearNum.ToString();
            if (GameMGR.Instance.sphereList.Count < 600)
            {
                if (co_Invoke != null) StopCoroutine(co_Invoke);
                co_Invoke = StartCoroutine(Invoke_End());
            }
        }
        else
        {
            if (clearText.text == "Clear") return;
            Sphere[] sp = FindObjectsOfType<Sphere>();
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].GetComponent<SphereCollider>().radius = 0.8f;
            }
            clearText.text = "Clear";
        }
    }
    IEnumerator Invoke_End()//공을 다 못채웠을시 3초후 재시작가능
    {
        yield return new WaitForSeconds(3f);
        GameMGR.Instance.ReStartUI();
    }
}
