using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject thermometer;
    private GameObject temperature;
    private LevelManager levelManager;

    void Start()
    {
        thermometer = this.transform.Find("Thermometer").gameObject;
        temperature = thermometer.transform.Find("Temperature").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager == null)
        {
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }

        temperature.transform.localScale = new Vector3(
            ((float)levelManager.GetCurrentTemperature() / levelManager.GetMaxTemperature()),
            1.0f,
            1.0f
        );
    }
}

