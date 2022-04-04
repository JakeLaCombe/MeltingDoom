using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public IInputable input;
    void Start()
    {
        input = GetComponent<IInputable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.Pause())
        {
            SceneManager.LoadScene("Main Game");
        }
    }
}
