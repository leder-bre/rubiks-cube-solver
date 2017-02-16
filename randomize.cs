using UnityEngine;
using System.Collections;

public class randomize : MonoBehaviour {
	private GameObject gamecontroller;
	// Use this for initialization
	void Start () {
		gamecontroller = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {}
	void OnMouseDown () {
		print ("RANDOM");
		gamecontroller.GetComponent<controller> ().assignIndex ();
		gamecontroller.GetComponent<controller> ().randomize ();
	}
}
