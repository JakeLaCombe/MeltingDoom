using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public PickUpType type;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        if (type == PickUpType.ICE_CUBE)
        {
            manager.decreaseTemperature(1);
        }
        else
        {
            manager.AddCoins(1);
        }

        Destroy(this.gameObject);
    }
}

public enum PickUpType
{
    COIN,
    ICE_CUBE
}