using UnityEngine;
using System.Collections;

public class solveButton : MonoBehaviour {
	GameObject gameController;
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		print ("CLICKED");
		gameController.GetComponent<controller> ().solveCube();//run = true;
	}
}