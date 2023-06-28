using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    Dictionary<string, List<Sphere>> table = new Dictionary<string, List<Sphere>>();

    public Sphere CreatePrefab(Sphere prefab, Vector3 position, Quaternion rotation)//Sphere 클래스 딕셔너리 오브젝트 풀링 
    {
        List<Sphere> list = null;
        Sphere instance = null;
        bool listCheck = table.TryGetValue(prefab.name.Replace("(Clone)", ""), out list);
        if (listCheck == false)
        {
            list = new List<Sphere>();
            table.Add(prefab.name.Replace("(Clone)", ""), list);
        }
        if (list.Count == 0)
        {
            instance = GameObject.Instantiate<Sphere>(prefab, position, rotation); 
            Debug.Log(prefab.name.Replace("(Clone)", ""));
        }
        else if (list.Count > 0)
        {
            instance = list[0];
            list.RemoveAt(0);
        }

        if (instance != null)
        {
            if(GameMGR.Instance.sphereColor == SphereColor.blue)
            {
                GameMGR.Instance.ChangeColor(instance, SphereColor.blue);
            }
            else if(GameMGR.Instance.sphereColor == SphereColor.yellow)
            {
                GameMGR.Instance.ChangeColor(instance, SphereColor.yellow);
            }
                instance.gameObject.SetActive(true);
            return instance;
        }
        else { return null; }
    }
    public void DestroyPrefab(Sphere Prefab)
    {
        List<Sphere> list = null;
        string prefabld = Prefab.name.Replace("(Clone)", "");
        bool listCached = table.TryGetValue(prefabld, out list);
        if (listCached == false)
        {
            Debug.LogError("Not Found" + Prefab.name);
            return;
        }
        Prefab.gameObject.SetActive(false);
        list.Add(Prefab);
    }
}
