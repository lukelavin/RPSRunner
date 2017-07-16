using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    private GameController gameController;

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("GameControl").Length > 0) {
            gameController = GameObject.FindGameObjectsWithTag("GameControl")[0].GetComponent<GameController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(-(float) (0.25 + (gameController.GetScore() * .010)), 0, 0);
	}
}
