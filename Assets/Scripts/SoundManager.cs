using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance = null;
    public AudioSource JUMP;
    public AudioSource COIN;
    public AudioSource ICE_CUBE;
    public AudioSource LEMONADE;
    public AudioSource MELTING;
    public AudioSource FALLING;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
