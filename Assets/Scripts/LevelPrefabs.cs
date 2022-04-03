using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levelParts = { };
    public static LevelPrefabs instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
