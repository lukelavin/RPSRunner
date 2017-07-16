using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject player;
    private PlayerController playerController;

    public GameObject randomizerLine;

    public GUIText scoreText;
    private int score;

    public GUIText gameOverText;
    public GUIText resetText;
    public GUIText quitText;
    public GUIText highScoreText;

    private bool gameOver;

    // Use this for initialization
    void Start () {
        playerController = player.GetComponent<PlayerController>();
        UpdateHighScore();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("_Scenes/Game");
                GameObject[] allObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
                foreach (GameObject obj in allObjects) {
                    if (!obj.CompareTag("GameControl") && !obj.CompareTag("DisplayText") && !obj.CompareTag("MainCamera")) {
                        obj.SetActive(true);
                        gameOver = false;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) {
                GameObject[] allObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
                foreach (GameObject obj in allObjects) {
                    if (!obj.CompareTag("GameControl") && !obj.CompareTag("DisplayText") && !obj.CompareTag("MainCamera")) {
                        obj.SetActive(true);
                    }
                }
                Application.Quit();
            }
        }
    }

    private void UpdateHighScore() {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore");
    }

    public void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public int GetScore() {
        return score;
    }

    public void SetScore(int newScore) {
        score = newScore;
        UpdateScore();
    }

    public void AddScore(int added) {
        SetScore(GetScore() + added);
        UpdateScore();
    }

    public void SpawnNewPickUps() {
        List<GameObject> prefabs = new List<GameObject>();
        prefabs.Add(playerController.rockPrefab);
        prefabs.Add(playerController.paperPrefab);
        prefabs.Add(playerController.scissorsPrefab);

        int firstPickUp = Random.Range(0, 3);
        Instantiate(prefabs[firstPickUp], new Vector3(20, 10, 0), Quaternion.identity);
        prefabs.RemoveAt(firstPickUp);

        int secondPickUp = Random.Range(0, 2);
        Instantiate(prefabs[secondPickUp], new Vector3(20, 0, 0), Quaternion.identity);
        prefabs.RemoveAt(secondPickUp);

        Instantiate(prefabs[0], new Vector3(20, -10, 0), Quaternion.identity);
        prefabs.RemoveAt(0);

        if (Random.Range(0, 3) == 0)
            Instantiate(randomizerLine);
    }


    public void EndGame() {
        print("End Game");
        GameObject[] allObjects = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject obj in allObjects) {
            if (!obj.CompareTag("GameControl") && !obj.CompareTag("DisplayText") && !obj.CompareTag("MainCamera")) {
                obj.SetActive(false);
            }
        }

        if (score > PlayerPrefs.GetInt("highscore")) {
            PlayerPrefs.SetInt("highscore", score);
        }

        UpdateHighScore();
        gameOverText.text = "Game Over";
        resetText.text = "Retry (R)";
        quitText.text = "Quit (ESC)";

        gameOver = true;
    }
}
