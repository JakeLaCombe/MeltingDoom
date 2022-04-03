using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonadeStand : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");

        if (other.tag != "Player")
        {
            return;
        }

        Debug.Log("Triggered With Player");

        this.GetComponent<BoxCollider2D>().enabled = false;

        GameObject levelManager = GameObject.FindWithTag("Level Manager");

        if (levelManager != null)
        {
            levelManager.GetComponent<LevelManager>().SetSpawnPoint(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z));
        }
    }
}
