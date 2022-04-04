using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;

    public GameObject confiner;

    public List<GameObject> currentLevelParts;

    private int[] level = { 0, 1, 3, 2, 1, 2, 0, 0, 2 };

    private int currentLevel = 0;

    public Vector3 currentStopPoint;

    public Vector3 spawnPoint;

    private int currentTemperature = 0;

    private int maxTemperature = 100;

    public Coroutine increaseTemperatureCoroutine;

    public LevelState currentState;

    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        confiner = GameObject.Find("CameraConfiner");
        currentStopPoint = player.transform.position;
        spawnPoint = currentStopPoint;

        initializeParts();
        currentState = LevelState.PLAYING;
        coins = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == LevelState.PLAYING)
        {
            if (player.transform.position.y < -10)
            {
                player.GetComponent<Player>().respawnPlayer(spawnPoint);
                return;
            }
            if (player.transform.position.x > currentStopPoint.x)
            {
                confiner.transform.position = new Vector3(
                    player.transform.position.x,
                    confiner.transform.position.y,
                    confiner.transform.position.z
                );
            }

            Transform nextPartPosition = currentLevelParts[currentLevelParts.Count - 2].transform.Find("End Position");

            if (nextPartPosition != null && player.transform.position.x + 5.0f > nextPartPosition.position.x)
            {
                Transform endPosition = currentLevelParts[currentLevelParts.Count - 1].transform.Find("End Position");
                GameObject nextPart = GameObject.Instantiate(LevelPrefabs.instance.levelParts[level[currentLevel]], new Vector3(endPosition.position.x, 0.0f, 0.0f), Quaternion.identity);
                currentLevel += 1;
                currentLevelParts.Add(nextPart);

                if (currentLevel >= level.Length)
                {
                    currentLevel = 0;
                }

                if (currentLevelParts.Count > 4)
                {
                    GameObject.Destroy(currentLevelParts[0]);
                    currentLevelParts.RemoveAt(0);
                }

                currentStopPoint = currentLevelParts[1].transform.position;
            }

            if (currentTemperature > maxTemperature)
            {
                currentState = LevelState.MELTED;
                player.GetComponent<Player>().melt();
            }
            else if (increaseTemperatureCoroutine == null)
            {
                increaseTemperatureCoroutine = StartCoroutine(increaseTemperature());
            }
        }


    }

    public void SetSpawnPoint(Vector3 position)
    {
        spawnPoint = position;
    }

    private void initializeParts()
    {
        GameObject firstPart = GameObject.Instantiate(LevelPrefabs.instance.levelParts[level[currentLevel]], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        currentLevelParts = new List<GameObject>();
        currentLevelParts.Add(firstPart);

        Transform firstObjectTransform = firstPart.transform.Find("End Position");
        GameObject firstBuffer = GameObject.Instantiate(
            LevelPrefabs.instance.levelParts[level[currentLevel]],
            new Vector3(
                -firstObjectTransform.position.x,
                0.0f,
                0.0f
            ),
            Quaternion.identity
        );
        currentLevelParts.Insert(0, firstBuffer);

    }

    public IEnumerator increaseTemperature()
    {
        currentTemperature += 1;
        yield return new WaitForSeconds(0.5f);
        increaseTemperatureCoroutine = null;
    }

    public int GetMaxTemperature()
    {
        return maxTemperature;
    }

    public int GetCurrentTemperature()
    {
        return currentTemperature;
    }

    public void decreaseTemperature(int amount)
    {
        currentTemperature -= amount;
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    public void RemoveCoins(int coins)
    {
        this.coins -= coins;
    }

    public int GetCoins()
    {
        return this.coins;
    }
}


public enum LevelState
{
    INITIAL,
    PLAYING,
    MELTED,
}