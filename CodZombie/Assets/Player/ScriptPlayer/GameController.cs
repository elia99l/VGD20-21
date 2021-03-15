using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject targetToSpawn;
    public GameObject targetSpawnPoint;

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TargetSpawnerCoroutine");
    }

    public void IncreaseScore()
    {
        score++;
        print("Score: " + score);
    }

    public IEnumerator TargetSpawnerCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPosition;
            spawnPosition = new Vector3(
                targetSpawnPoint.transform.position.x + Random.Range(-5.0f, 5.0f),
                targetSpawnPoint.transform.position.y + Random.Range(-5.0f, 5.0f),
                targetSpawnPoint.transform.position.z
            );
            GameObject.Instantiate(targetToSpawn, spawnPosition, transform.rotation);
            yield return new WaitForSeconds(1.0f);
        }
        if (PlayerPrefs.GetInt("Score", -1) < score)
        {
            //New highscore!
            print("New highscore: " + score);
            PlayerPrefs.SetInt("Score", score);
        }
      //  SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
