using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;

    public GameObject confiner;
    public List<GameObject> currentLevelParts;

    private int[] level = { 0, 1, 0, 0, 1, 1 };

    private int currentLevel = 0;

    public Vector3 currentStopPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        confiner = GameObject.Find("CameraConfiner");

        currentStopPoint = player.transform.position;
        initializeParts();
    }

    // Update is called once per frame
    void Update()
    {
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

            if (currentLevel > level.Length)
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
}
