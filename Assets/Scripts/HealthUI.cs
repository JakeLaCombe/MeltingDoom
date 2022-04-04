using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject thermometer;
    private GameObject temperature;
    private LevelManager levelManager;
    private GameObject gameOver;
    private TextMeshProUGUI coins;
    private GameObject pause;

    void Start()
    {
        thermometer = this.transform.Find("Thermometer").gameObject;
        temperature = thermometer.transform.Find("Temperature").gameObject;
        coins = this.transform.Find("Coins").GetComponent<TextMeshProUGUI>();
        gameOver = this.transform.Find("Game Over").gameObject;
        pause = this.transform.Find("Paused").gameObject;
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

        coins.text = "x " + levelManager.GetCoins();

        gameOver.SetActive(levelManager.GetMaxTemperature() < levelManager.GetCurrentTemperature());
        pause.SetActive(Time.timeScale <= 0.0f);
    }
}

