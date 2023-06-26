using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [field: SerializeField] public float gameProgress { get; private set; }
    private void Awake()
    {
        
        StartCoroutine(COR_MoveCam());

    }
    IEnumerator COR_MoveCam()
    {
        yield return null;
    }
    void Start()
    {
        
    }
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x, gameProgress, transform.position.z);
    }
}
