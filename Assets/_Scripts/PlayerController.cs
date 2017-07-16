using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;
    public GameObject rockPrefab;
    public GameObject paperPrefab;
    public GameObject scissorsPrefab;

    public GameObject gameControl;
    private GameController gameController;
    
    void Start() {
        gameController = gameControl.GetComponent<GameController>();

        int sprite = Random.Range(0, 3);
        switch (sprite) {
            case 0:
                GetComponent<SpriteRenderer>().sprite = rockSprite;
                tag = "RockPlayer";
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = paperSprite;
                tag = "PaperPlayer";
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = scissorsSprite;
                tag = "ScissorsPlayer";
                break;
        }

        gameController.SpawnNewPickUps();
    }
     
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            if (transform.position.y != 10) {
                transform.position = transform.position + new Vector3(0, 10, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            if (transform.position.y != -10) {
                transform.position = transform.position + new Vector3(0, -10, 0);
            }
        }
    }

    void RandomizePlayerSprite() {
        print("Randomize");
        string[] tags = { "RockPlayer", "PaperPlayer", "ScissorsPlayer"};
        List<string> tagList = new List<string>(tags);
        tagList.RemoveAt(tagList.IndexOf(tag));
        int sprite = Random.Range(0, tagList.Count);
        
        switch (tagList[sprite]) {
            case "RockPlayer":
                GetComponent<SpriteRenderer>().sprite = rockSprite;
                tag = "RockPlayer";
                break;
            case "PaperPlayer":
                GetComponent<SpriteRenderer>().sprite = paperSprite;
                tag = "PaperPlayer";
                break;
            case "ScissorsPlayer":
                GetComponent<SpriteRenderer>().sprite = scissorsSprite;
                tag = "ScissorsPlayer";
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        bool spawn = true;

        switch (other.tag) {
            case "Rock":
                GetComponent<SpriteRenderer>().sprite = rockSprite;
                if(CompareTag("PaperPlayer")) {
                    gameController.AddScore(1);
                    tag = "RockPlayer";                    
                }           
                else {
                    gameController.EndGame();
                }            
                break;
            case "Paper":
                GetComponent<SpriteRenderer>().sprite = paperSprite;
                if (CompareTag("ScissorsPlayer")) {
                    gameController.AddScore(1);
                    tag = "PaperPlayer";
                }
                else {
                    gameController.EndGame();
                }            
                break;
            case "Scissors":               
                GetComponent<SpriteRenderer>().sprite = scissorsSprite;
                if (CompareTag("RockPlayer")) {
                    gameController.AddScore(1);
                    tag = "ScissorsPlayer";
                }
                else {
                    gameController.EndGame();
                }
                break;
            case "Randomizer":
                print("collision with Randomizer");
                RandomizePlayerSprite();
                other.gameObject.SetActive(false);
                spawn = false;
                break;
        }

        if (spawn) {
            GameObject[] objects = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (GameObject obj in objects) {
                if (obj.CompareTag("Rock") || obj.CompareTag("Paper") || obj.CompareTag("Scissors")) {
                    Destroy(obj.GetComponent<Rigidbody2D>());
                    Destroy(obj.GetComponent<BoxCollider2D>());
                }
            }

            gameController.SpawnNewPickUps();
        }

        other.gameObject.SetActive(false);
    }
}
