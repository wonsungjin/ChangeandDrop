using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Awake()
    {
        
    }
}
