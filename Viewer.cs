using UnityEngine;
using System.Collections;

public class Viewer : MonoBehaviour {
	GameObject cube;
	GameObject red;
	GameObject orange;
	GameObject green;
	GameObject blue;
	GameObject white;
	GameObject yellow;
	// Use this for initialization
	void Start () {
		cube = GameObject.Find ("Cube");
		red = GameObject.Find ("Red");
		orange = GameObject.Find ("Orange");
		green = GameObject.Find ("Green");
		blue = GameObject.Find ("Blue");
		yellow = GameObject.Find ("Yellow");
		white = GameObject.Find ("White");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.DownArrow)) {
			cube.transform.Rotate (Vector3.left * (Time.deltaTime * 500));
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			cube.transform.Rotate (Vector3.right * (Time.deltaTime * 500));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			cube.transform.Rotate (Vector3.up * (Time.deltaTime * 500));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			cube.transform.Rotate (Vector3.down * (Time.deltaTime * 500));
		}
		if (Input.GetKey (KeyCode.Z)) {
			cube.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			red.transform.localPosition = new Vector3 (3.5f, 0f, 0f);
			red.transform.localEulerAngles = new Vector3 (0, 0, 0);
			orange.transform.localPosition = new Vector3 (-3.5f, 0f, 0f);
			orange.transform.localEulerAngles = new Vector3 (0, 0, 0);
			blue.transform.localPosition = new Vector3 (0f, 3.5f, 0f);
			blue.transform.localEulerAngles = new Vector3 (0, 0, 0);
			green.transform.localPosition = new Vector3 (0f, -3.5f, 0f);
			green.transform.localEulerAngles = new Vector3 (0, 0, 0);
			yellow.transform.localPosition = new Vector3 (-7f, 0f, 0f);
			yellow.transform.localEulerAngles = new Vector3 (0, 0, 0);
			white.transform.localPosition = new Vector3 (0, 0, 0);
			white.transform.localEulerAngles = new Vector3 (0, 0, 0);
		}
		if (Input.GetKey (KeyCode.X)) {
			cube.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			red.transform.localPosition = new Vector3 (1.75f, 0f, 1.75f);
			red.transform.localEulerAngles = new Vector3 (0, -90, 0);
			orange.transform.localPosition = new Vector3 (-1.75f, 0f, 1.75f);
			orange.transform.localEulerAngles = new Vector3 (0, 90, 0);
			blue.transform.localPosition = new Vector3 (0f, 1.75f, 1.75f);
			blue.transform.localEulerAngles = new Vector3 (90, 0, 0);
			green.transform.localPosition = new Vector3 (0f, -1.75f, 1.75f);
			green.transform.localEulerAngles = new Vector3 (-90, 0, 0);
			yellow.transform.localPosition = new Vector3 (0f, 0f, 3.5f);
			yellow.transform.localEulerAngles = new Vector3 (0, 180, 0);
			white.transform.localPosition = new Vector3 (0, 0, 0);
			white.transform.localEulerAngles = new Vector3 (0, 0, 0);
		}
	}
}
