using UnityEngine;
using System.Collections;
public class colorChange : MonoBehaviour {
	public int currentColor; // 0:White, 1:Yellow, 2:Red, 3:Orange, 4:Blue, 5:Green
	public bool test = true;
	public int address0 = 0;
	public int address1 = 0;
	GameObject gamecontroller;
	public Renderer rend;
	// Use this for initialization
	void Start () {
		gamecontroller = GameObject.FindGameObjectWithTag ("GameController");
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		string color = gameObject.transform.parent.gameObject.name;
		switch (color) {
		case "White":
			currentColor = 0;
			break;
		case "Yellow":
			currentColor = 1;
			break;
		case "Red":
			currentColor = 2;
			break;
		case "Orange":
			currentColor = 3;
			break;
		case "Blue":
			currentColor = 4;
			break;
		case "Green":
			currentColor = 5;
			break;
		default:
			print ("Error: Color not detected");
			break;
		}
	}
	// Update is called once per frame
	void Update () {
		if (gamecontroller.GetComponent<controller> ().run > 0) {
			//print ("A: " + currentColor);
			currentColor = gamecontroller.GetComponent<controller> ().sideColorList [address0, address1];
			//print ("B: " + currentColor);
			rend.sharedMaterial = gamecontroller.GetComponent<controller> ().materials [currentColor];
			gamecontroller.GetComponent<controller> ().run--;
		}
	}
	public void OnMouseDown() {
		currentColor = (currentColor + 1) % 6;
		rend.sharedMaterial = gamecontroller.GetComponent<controller> ().materials [currentColor];
	}
}