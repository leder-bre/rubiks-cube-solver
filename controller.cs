using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour {
	public Material[] materials;
	public int run;
	public string sequence = "";
	private bool blueEdge = false;
	private bool redEdge = false;
	private bool greenEdge = false;
	private bool orangeEdge = false;
	private bool greenRed = false;
	private bool redBlue = false;
	private bool blueOrange = false;
	private bool orangeGreen = false;
	private bool greenRedEdge = false;
	private bool redBlueEdge = false;
	private bool blueOrangeEdge = false;
	private bool orangeGreenEdge = false;
	private bool topCornersTotal = false;
	private bool doWait = false;
	public int [,] sideColorList = new int[6,8];
	// Use this for initialization
	void Start () {
		run = 0;
	}
	// Update is called once per frame
	void Update () {
	}
	public void randomize () {
		for (int i = 0; i < 20; i++) {
			int randomNum = (int) Mathf.Round(UnityEngine.Random.Range (0, 11));
			switch (randomNum) {
			case 0:
				F ();
				break;
			case 1:
				Fi ();
				break;
			case 2:
				B ();
				break;
			case 3:
				Bi ();
				break;
			case 4:
				R ();
				break;
			case 5:
				Ri ();
				break;
			case 6:
				L ();
				break;
			case 7:
				Li ();
				break;
			case 8:
				U ();
				break;
			case 9:
				Ui ();
				break;
			case 10:
				D ();
				break;
			case 11:
				Di ();
				break;
			default:
				break;
			}
		}
		sequence = "";
	}
	public void wait() {
		Transform cube = GameObject.Find ("Cube").transform;
		int[,] temp_gameObjects = new int[6,8];
		int sideNum = 0;
		foreach (Transform aSide in cube) {	//Find each side
			if (aSide != cube && aSide.gameObject.name != "Inside") {
				for (int i = 1; i < aSide.childCount; i++) {
					aSide.GetChild (i).gameObject.GetComponent<colorChange>().currentColor--;
					aSide.GetChild (i).gameObject.GetComponent<colorChange>().OnMouseDown();
				}
				sideNum++;
			}
		}
		doWait = true;
		//DateTime dt = DateTime.Now + TimeSpan.FromMilliseconds (100);
		//do {} while (DateTime.Now < dt);
	}
	void LateUpdate() {
		if (doWait) {
			DateTime dt = DateTime.Now + TimeSpan.FromMilliseconds (100);
			do {} while (DateTime.Now < dt);
		}
	}
	public void assignIndex() {
		Transform cube = GameObject.Find ("Cube").transform;
		int[,] temp_gameObjects = new int[6,8];
		int sideNum = 0;
		foreach (Transform aSide in cube) {	//Find each side
			int index1 = 0;
			int index2 = 0;
			switch (aSide.gameObject.name) {

			case "White":
				index1 = 0;
				break;
			case "Yellow":
				index1 = 1;
				break;
			case "Blue":
				index1 = 2;
				break;
			case "Green":
				index1 = 3;
				break;
			case "Red":
				index1 = 4;
				break;
			case "Orange":
				index1 = 5;
				break;
			default:
				break;
			}
			if (aSide != cube && aSide.gameObject.name != "Inside") {
				for (int i = 1; i < aSide.childCount; i++) {
					switch (aSide.GetChild (i).gameObject.GetComponent<colorChange> ().name [7]) {
					case '3':
						index2 = 0;
						break;
					case '5':
						index2 = 1;
						break;
					case '4':
						index2 = 2;
						break;
					case '1':
						index2 = 3;
						break;
					case '7':
						index2 = 4;
						break;
					case '6':
						index2 = 5;
						break;
					case '8':
						index2 = 6;
						break;
					case '2':
						index2 = 7;
						break;
					default:
						break;
					}
					aSide.GetChild (i).gameObject.GetComponent<colorChange> ().address0 = index1;
					aSide.GetChild (i).gameObject.GetComponent<colorChange> ().address1 = index2;
					temp_gameObjects[sideNum,i-1] = (aSide.GetChild (i).gameObject.GetComponent<colorChange>().currentColor);
				}
				sideNum++;
			}
		}
		for (int i = 0; i < 6; i++) {
			sideColorList [i,0] = temp_gameObjects [i,2];
			sideColorList [i,1] = temp_gameObjects [i,4];
			sideColorList [i,2] = temp_gameObjects [i,3];
			sideColorList [i,3] = temp_gameObjects [i,0];
			sideColorList [i,4] = temp_gameObjects [i,6];
			sideColorList [i,5] = temp_gameObjects [i,5];
			sideColorList [i,6] = temp_gameObjects [i,7];
			sideColorList [i,7] = temp_gameObjects [i,1];
		}
	}
	public void solveCube() {
		assignIndex ();
		blueEdge = false;
		redEdge = false;
		greenEdge = false;
		orangeEdge = false;
		greenRed = false;
		redBlue = false;
		blueOrange = false;
		orangeGreen = false;
		greenRedEdge = false;
		redBlueEdge = false;
		blueOrangeEdge = false;
		orangeGreenEdge = false;
		topCornersTotal = false;
		print ("Solving White Edges");
		whiteEdges ();
		if (blueEdge && redEdge && orangeEdge && greenEdge) {
			print ("White Edges Solves, solving white corners");
			whiteCorners ();
			if (greenRed && redBlue && blueOrange && orangeGreen) {
				print ("White corners solved, solving middle");
				middleEdges ();
				if (blueOrangeEdge && redBlueEdge && greenRedEdge && orangeGreenEdge) {
					print ("Middle edges solved, solving top corners");
					topCorners ();
					if (topCornersTotal) {
						print ("Top corners solved, solving top edges");
						topEdges ();
						if (blueYellow () && greenYellow () && redYellow () && orangeYellow ()) {
							print ("Cube solved");
							cleanSequence ();
						}
					}
				}
			}
		}
	}
//	public void wait() {
//		DateTime dt = DateTime.Now + TimeSpan.FromMilliseconds (10);
//		do {} while (DateTime.Now < dt);
//	}
	void cleanSequence() {
		for (int i = 0; i < sequence.Length - 4; i++) {
			if (i < sequence.Length - 4) {
				if (sequence [i] == '\n') {
					sequence = sequence.Remove (i, 1);
				}
				if (sequence [i] == sequence [i + 1] && sequence [i + 1] == sequence [i + 2] && sequence [i + 2] == sequence [i + 3]) {
					sequence = sequence.Remove (i, 4);
				}
				if (sequence [i] == sequence [i + 1] && sequence [i + 1] == sequence [i + 2] && sequence [i + 3] != 'i') {
					sequence = sequence.Remove (i + 1, 2);
				}
				if (sequence [i] == sequence [i + 1] && sequence [i + 2] == 'i') {
					sequence = sequence.Remove (i, 2);
				}
			}
		}
		for (int i = 0; i < sequence.Length; i++) {
			if (i % 60 == 0 && i != 0) {
				sequence = sequence.Insert (i, "\n");
			}
		}
		GameObject.Find ("SeqText").GetComponent<TextMesh> ().text = sequence;
	}
	void topEdges() {
		topEdgePositions ();
		if (blueYellowPosition() && redYellowPosition() && orangeYellowPosition() && greenYellowPosition()) {
			topEdgeOrientation ();
		}
	}
	void topEdgeOrientation() {
		if (blueYellow () && greenYellow () && !redYellow () && !orangeYellow ()) {
			HPerm ();
		}
		if (!blueYellow () && !greenYellow () && redYellow () && orangeYellow ()) {
			U ();
			HPerm ();
			Ui ();
		}
		if (!blueYellow () && greenYellow () && redYellow () && !orangeYellow ()) {
			U ();
			U ();
			LPerm ();
			U ();
			U ();
		}
		if (!blueYellow () && greenYellow () && !redYellow () && orangeYellow ()) {
			Ui ();
			LPerm ();
			U ();
		}
		if (blueYellow () && !greenYellow () && redYellow () && !orangeYellow ()) {
			U ();
			LPerm();
			Ui ();
		}
		if (blueYellow () && !greenYellow () && !redYellow () && orangeYellow ()) {
			LPerm();
		}
		if (!blueYellow () && !greenYellow () && !redYellow () && !orangeYellow ()) {
			HPerm ();
		}
	}
	void HPerm() {
		Ri ();
		Ui ();
		D ();
		B ();
		B ();
		U ();
		U ();
		D ();
		D ();
		Fi ();
		U ();
		U ();
		F ();
		U ();
		U ();
		D ();
		D ();
		B ();
		B ();
		U ();
		Di ();
		R ();
		U ();
		U ();
	}
	void LPerm() {
		F ();
		Ui ();
		D ();
		B ();
		B ();
		U ();
		U ();
		D ();
		D ();
		Fi ();
		U ();
		U ();
		F ();
		U ();
		U ();
		D ();
		D ();
		B ();
		B ();
		U ();
		Di ();
		R ();
		U ();
		U ();
		Ri ();
		Fi ();
	}
	bool blueYellow() {
		return (sideColorList [2, 1] == 4 && sideColorList [1, 1] == 1);
	}
	bool greenYellow() {
		return (sideColorList [3, 5] == 5 && sideColorList [1, 5] == 1);
	}
	bool redYellow() {
		return (sideColorList [4, 3] == 2 && sideColorList [1, 7] == 1);
	}
	bool orangeYellow() {
		return (sideColorList [5, 7] == 3 && sideColorList [1, 3] == 1);
	}
	void topEdgePositions() {
		if (blueYellowPosition() && !(greenYellowPosition()) && !(redYellowPosition()) && !(orangeYellowPosition())) {
			CCTopEdgeRotation ();
		}
		if (!blueYellowPosition() && greenYellowPosition() && !redYellowPosition() && !orangeYellowPosition()) {
			U ();
			U ();
			CCTopEdgeRotation ();
			U ();
			U ();
		}
		if (!blueYellowPosition () && !greenYellowPosition () && redYellowPosition () && !orangeYellowPosition ()) {
			U ();
			CCTopEdgeRotation ();
			Ui ();
		}
		if (!blueYellowPosition () && !greenYellowPosition () && !redYellowPosition () && orangeYellowPosition ()) {
			Ui ();
			CCTopEdgeRotation ();
			U ();
		}
		if (!(blueYellowPosition () && greenYellowPosition () && redYellowPosition () && orangeYellowPosition ())) {
			CCTopEdgeRotation ();
		}
	}
	void CCTopEdgeRotation() {
		L ();
		Ri ();
		F ();
		Li ();
		R ();
		U ();
		U ();
		L ();
		Ri ();
		F ();
		R ();
		Li ();
	}
	bool blueYellowPosition() {
		return ((sideColorList [2, 1] == 1 || sideColorList [2, 1] == 4) && (sideColorList [1, 1] == 4 || sideColorList [1, 1] == 1));
	}
	bool greenYellowPosition() {
		return ((sideColorList [3, 5] == 5 || sideColorList [3, 5] == 1) && (sideColorList [1, 5] == 5 || sideColorList [1, 5] == 1));
	}
	bool redYellowPosition() {
		return ((sideColorList [4, 3] == 2 || sideColorList [4, 3] == 1) && (sideColorList [1, 7] == 2 || sideColorList [1, 7] == 1));
	}
	bool orangeYellowPosition() {
		return ((sideColorList [5, 7] == 3 || sideColorList [5, 7] == 1) && (sideColorList [1, 3] == 3 || sideColorList [1, 3] == 1));
	}
	void topCorners() {
		topCornerPosition ();
		if (topCornerPositions()) {
			topCornerOrientation ();
		}
	}
	void topCornerOrientation() {
		for (int i = 0; i < 4; i++) {
			if (sideColorList [1, 0] != 1 && sideColorList [1, 2] != 1 && sideColorList [1, 4] == 1 && sideColorList [1, 6] == 1) {
				if (sideColorList [2, 0] == 1 && sideColorList [2, 2] == 1) {
					sine ();
					antisine ();
				} else {
					U ();
					sine ();
					U ();
					U ();
					antisine ();
					U ();
				}
			} else if (sideColorList [4, 2] == 1 && sideColorList [5, 0] == 1 && sideColorList [3, 4] == 1 && sideColorList [3, 6] == 1) {
				sine ();
				Ui ();
				sine ();
				U ();
			} else if (sideColorList [4, 2] == 1 && sideColorList [4, 4] == 1 && sideColorList [5, 6] == 1 && sideColorList [5, 0] == 1) {
				sine ();
				U ();
				U ();
				sine ();
				U ();
				U ();
			} else if (sideColorList [2, 0] == 1 && sideColorList [4, 4] == 1 && sideColorList [1, 0] == 1 && sideColorList [1, 4] == 1) {
				sine ();
				U ();
				antisine ();
				Ui ();
			} else if (sideColorList [2, 2] == 1 && sideColorList [3, 6] == 1 && sideColorList [1, 2] == 1 && sideColorList [1, 6] == 1) {
				antisine ();
				Ui ();
				sine ();
				U ();
			} else if (sideColorList [1, 0] != 1 && sideColorList [1, 2] == 1 && sideColorList [1, 4] != 1 && sideColorList [1, 6] != 1) {
				sine ();
			} else if (sideColorList [1, 0] == 1 && sideColorList [1, 2] != 1 && sideColorList [1, 4] != 1 && sideColorList [1, 6] != 1) {
				antisine ();
			} else if (sideColorList [1, 0] == 1 && sideColorList [1, 6] == 1 && sideColorList [2, 0] == 1 && sideColorList [3, 6] == 1) {
				sine ();
				U ();
				U ();
				antisine ();
				U ();
				U ();
			}
			U ();
		}
		if (greenRedCorner () && greenOrangeCorner () && blueRedCorner () && blueOrangeCorner ()) {
			topCornersTotal = true;
		}
	}
	void sine() {
		R ();
		U ();
		Ri ();
		U ();
		R ();
		U ();
		U ();
		Ri ();
		U ();
		U ();
	}
	void antisine() {
		Li ();
		U ();
		L ();
		U ();
		Li ();
		U ();
		U ();
		L ();
		U ();
		U ();
	}
	void topCornerPosition() {
		if/*while*/ (!blueRedCornerPosition ()) {
			U ();
		} else {
			if (!(blueOrangeCornerPosition() && greenOrangeCornerPosition() && greenOrangeCornerPosition())) {
				Bi ();
				R ();
				Bi ();
				L ();
				L ();
				B ();
				Ri ();
				Bi ();
				L ();
				L ();
				B ();
				B ();
			}
			if (blueOrangeCornerPosition() && !greenOrangeCornerPosition() && !greenRedCornerPosition()) {
				Ri ();
				Ui ();
				R ();
				B ();
				U ();
				Bi ();
				Ri ();
				U ();
				R ();
				U ();
				U ();
			}
			if (greenOrangeCornerPosition() && !greenRedCornerPosition() && !blueOrangeCornerPosition()) {
				Ri ();
				Ui ();
				R ();
				B ();
				U ();
				Bi ();
				Ri ();
				U ();
				R ();
				U ();
				U ();
			}
			if (greenRedCornerPosition () && !greenOrangeCornerPosition () && !greenRedCornerPosition ()) {
				Bi ();
				Ui ();
				B ();
				L ();
				U ();
				Li ();
				Bi ();
				U ();
				B ();
				U ();
				U ();
			}
		}
	}
	bool topCornerPositions() {
		return (greenRedCornerPosition () && greenOrangeCornerPosition () && blueRedCornerPosition () && blueOrangeCornerPosition ());
	}
	bool blueRedCornerPosition() {
		return ((sideColorList [1, 0] == 1 || sideColorList [1, 0] == 4 || sideColorList [1, 0] == 2) && (sideColorList [4, 2] == 1 || sideColorList [4, 2] == 4 || sideColorList [4, 2] == 2) && (sideColorList [2, 2] == 1 || sideColorList [2, 2] == 4 || sideColorList [2, 2] == 2));
	}
	bool blueRedCorner() {
		return	(sideColorList [1, 0] == 1 && sideColorList [4, 2] == 2 && sideColorList [2, 2] == 4);
	}
	bool blueOrangeCornerPosition() {
		return ((sideColorList [1, 2] == 1 || sideColorList [1, 2] == 4 || sideColorList [1, 2] == 3) && (sideColorList [5, 0] == 1 || sideColorList [5, 0] == 4 || sideColorList [5, 0] == 3) && (sideColorList [2, 0] == 1 || sideColorList [2, 0] == 4 || sideColorList [2, 0] == 3));
	}
	bool blueOrangeCorner() {
		return (sideColorList [1, 2] == 1 && sideColorList [5, 0] == 3 && sideColorList [2, 0] == 4);
	}
	bool greenOrangeCornerPosition() {
		return ((sideColorList [1, 4] == 1 || sideColorList [1, 4] == 5 || sideColorList [1, 4] == 3) && (sideColorList [5, 6] == 1 || sideColorList [5, 6] == 5 || sideColorList [5, 6] == 3) && (sideColorList [3, 6] == 1 || sideColorList [3, 6] == 5 || sideColorList [3, 6] == 3));
	}
	bool greenOrangeCorner() {
		return (sideColorList [1, 4] == 1 && sideColorList [5, 6] == 3 && sideColorList [3, 6] == 5);
	}
	bool greenRedCornerPosition() {
		return ((sideColorList [1, 6] == 1 || sideColorList [1, 6] == 5 || sideColorList [1, 6] == 2) && (sideColorList [4, 4] == 1 || sideColorList [4, 4] == 5 || sideColorList [4, 4] == 2) && (sideColorList [3, 4] == 1 || sideColorList [3, 4] == 5 || sideColorList [3, 4] == 2));
	}
	bool greenRedCorner() {
		return (sideColorList [1, 6] == 1 && sideColorList [4, 4] == 2 && sideColorList [3, 4] == 5);
	}
	void middleEdges() { // 0:White, 1:Yellow, 2:Red, 3:Orange, 4:Blue, 5:Green
		if (sideColorList [2, 3] == 4 && sideColorList [4, 1] == 2) {
			redBlueEdge = true;
		}
		if (sideColorList [2, 7] == 4 && sideColorList [5, 1] == 3) {
			blueOrangeEdge = true;
		}
		if (sideColorList [3, 3] == 5 && sideColorList [4, 5] == 2) {
			greenRedEdge = true;
		}
		if (sideColorList [3, 7] == 5 && sideColorList [5, 5] == 3) {
			orangeGreenEdge = true;
		}
		if/*while*/ (!(blueOrangeEdge && redBlueEdge && greenRedEdge && orangeGreenEdge)) {
			topMiddleEdge ();
			edgeMiddleEdge ();
		}
	}
	void edgeMiddleEdge() {
		if (sideColorList [2, 3] != 4 || sideColorList [4, 1] != 2) {
			U ();
			R ();
			Ui ();
			Ri ();
			Ui ();
			Fi ();
			U ();
			F ();
		}
		if (sideColorList [2, 7] != 4 || sideColorList [5, 1] != 3) {
			Ui ();
			Li ();
			U ();
			L ();
			U ();
			F ();
			Ui ();
			Fi ();
			topMiddleEdge ();
		}
		if (sideColorList [3, 3] != 5 || sideColorList [4, 5] != 2) {
			Ui ();
			Ri ();
			U ();
			R ();
			U ();
			B ();
			Ui ();
			Bi ();
			topMiddleEdge ();
		}
		if (sideColorList [3, 7] != 5 || sideColorList [5, 5] != 3) {
			U ();
			L ();
			Ui ();
			Li ();
			Ui ();
			Bi ();
			U ();
			B ();
			topMiddleEdge ();
		}
	}
	void topMiddleEdge() {
		bool foundSolve = false;
		for (int i = 0; i < 4; i++) {
			if (sideColorList [2, 1] == 4) { // Blue
				if (sideColorList [1, 1] == 2) {
					U ();
					R ();
					Ui ();
					Ri ();
					Ui ();
					Fi ();
					U ();
					F ();
					redBlueEdge = true;
					foundSolve = true;
				}
				if (sideColorList [1, 1] == 3) {
					Ui ();
					Li ();
					U ();
					L ();
					U ();
					F ();
					Ui ();
					Fi ();
					blueOrangeEdge = true;
					foundSolve = true;
				}
			}
			if (sideColorList [3, 5] == 5) { // Green
				if (sideColorList [1, 5] == 3) {
					U ();
					L ();
					Ui ();
					Li ();
					Ui ();
					Bi ();
					U ();
					B ();
					orangeGreenEdge = true;
					foundSolve = true;
				}
				if (sideColorList [1, 5] == 2) {
					Ui ();
					Ri ();
					U ();
					R ();
					U ();
					B ();
					Ui ();
					Bi ();
					greenRedEdge = true;
					foundSolve = true;
				}
			}
			if (sideColorList [4, 3] == 2) { // Red
				if (sideColorList [1, 7] == 4) {
					Ui ();
					Fi ();
					U ();
					F ();
					U ();
					R ();
					Ui ();
					Ri ();
					redBlueEdge = true;
					foundSolve = true;
				}
				if (sideColorList [1, 7] == 5) {
					U ();
					B ();
					Ui ();
					Bi ();
					Ui ();
					Ri ();
					U ();
					R ();
					greenRedEdge = true;
					foundSolve = true;
				}
			}
			if (sideColorList [5, 7] == 3) { // Orange
				if (sideColorList [1, 3] == 5) {
					Ui ();
					Bi ();
					U ();
					B ();
					U ();
					L ();
					Ui ();
					Li ();
					orangeGreenEdge = true;
					foundSolve = true;
				}
				if (sideColorList [1, 3] == 4) {
					U ();
					F ();
					Ui ();
					Fi ();
					Ui ();
					Li ();
					U ();
					L ();
					blueOrangeEdge = true;
					foundSolve = true;
				}
			}
			U ();
		}
		if (!foundSolve) {
			sequence = sequence.Remove (sequence.Length-4);
		}
	}
	void whiteCorners() {
		if (sideColorList [0, 0] == 0 && sideColorList [2, 6] == 4) {
			blueOrange = true;
		}
		if (sideColorList [0, 2] == 0 && sideColorList [2, 4] == 4) {
			redBlue = true;
		}
		if (sideColorList [0, 4] == 0 && sideColorList [3, 2] == 5) {
			greenRed = true;
		}
		if (sideColorList [0, 6] == 0 && sideColorList [3, 0] == 5) {
			orangeGreen = true;
		}
		if/*while*/ (!(greenRed && redBlue && blueOrange && orangeGreen)) {
			topOutWhiteCorner ();
			botDownWhiteCorner ();
			botOutWhiteCorner ();
			topUpWhiteCorner ();
		}
		blueOrange = false;
		if (sideColorList [0, 0] == 0 && sideColorList [2, 6] == 4) {
			blueOrange = true;
		}
		redBlue = false;
		if (sideColorList [0, 2] == 0 && sideColorList [2, 4] == 4) {
			redBlue = true;
		}
		greenRed = false;
		if (sideColorList [0, 4] == 0 && sideColorList [3, 2] == 5) {
			greenRed = true;
		}
		orangeGreen = false;
		if (sideColorList [0, 6] == 0 && sideColorList [3, 0] == 5) {
			orangeGreen = true;
		}
	}
	void whiteEdges() {
		if (sideColorList [2, 5] == 4 && sideColorList [0, 1] == 0) {
			blueEdge = true;
		}
		if (sideColorList [3, 1] == 5 && sideColorList [0, 5] == 0) {
			greenEdge = true;
		}
		if (sideColorList [4, 7] == 2 && sideColorList [0, 3] == 0) {
			redEdge = true;
		}
		if (sideColorList [5, 3] == 3 && sideColorList [0, 7] == 0) {
			orangeEdge = true;
		}
		if/*while*/ (!(blueEdge && redEdge && orangeEdge && greenEdge)) { // Solve white edges
			topWhiteEdge ();
			edgeWhiteEdge ();
			outBotWhiteEdge ();
			outTopWhiteEdge ();
			botWhiteEdge ();
			downWhiteEdge ();
		}
		blueEdge = false;
		if (sideColorList [2, 5] == 4 && sideColorList [0, 1] == 0) {
			blueEdge = true;
		}
		greenEdge = false;
		if (sideColorList [3, 1] == 5 && sideColorList [0, 5] == 0) {
			greenEdge = true;
		}
		redEdge = false;
		if (sideColorList [4, 7] == 2 && sideColorList [0, 3] == 0) {
			redEdge = true;
		}
		orangeEdge = false;
		if (sideColorList [5, 3] == 3 && sideColorList [0, 7] == 0) {
			orangeEdge = true;
		}
	}
	void F() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [2, i - 6] = oldInfo [2, i % 8];
		}
		for (int i = 0; i < 3; i++) {
			sideColorList [0, i] = oldInfo [4, i]; // white
			sideColorList [1, i] = oldInfo [5, i]; // yellow
			sideColorList [4, i] = oldInfo [1, i]; // red
			sideColorList [5, i] = oldInfo [0, i]; // orange
		}
		run = 48;
		sequence += "F";
		wait ();
	}

	void Fi() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [2, i] = oldInfo [2, (i+2) % 8];
		}
		for (int i = 0; i < 3; i++) {
			sideColorList [4, i] = oldInfo [0, i]; // white
			sideColorList [5, i] = oldInfo [1, i]; // yellow
			sideColorList [1, i] = oldInfo [4, i]; // red
			sideColorList [0, i] = oldInfo [5, i]; // orange
		}
		run = 48;
		sequence += "Fi";
		wait ();
	}
	void B() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [3, i - 6] = oldInfo [3, i % 8];
		}
		for (int i = 4; i < 7; i++) {
			sideColorList [4, i] = oldInfo [0, i]; // white
			sideColorList [5, i] = oldInfo [1, i]; // yellow
			sideColorList [1, i] = oldInfo [4, i]; // red
			sideColorList [0, i] = oldInfo [5, i]; // orange
		}
		run = 48;
		sequence += "B";
		wait ();
	}
	void Bi() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [3, i] = oldInfo [3, (i+2) % 8];
		}
		for (int i = 4; i < 7; i++) {
			sideColorList [0, i] = oldInfo [4, i]; // white
			sideColorList [1, i] = oldInfo [5, i]; // yellow
			sideColorList [4, i] = oldInfo [1, i]; // red
			sideColorList [5, i] = oldInfo [0, i]; // orange
		}
		run = 48;
		sequence += "Bi";
		wait ();
	}
	void L() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [5, i - 6] = oldInfo [5, i % 8];
		}
		for (int i = 6; i < 9; i++) {
			sideColorList [3, i%8] = oldInfo [0, i%8]; // green
			sideColorList [0, i%8] = oldInfo [2, i%8]; // white
		}
		for (int i = 6; i < 9; i++) {
			sideColorList [2, i%8] = oldInfo [1, ((i%8)+4)%8]; // blue
			sideColorList [1, ((i%8)+4)%8] = oldInfo [3, i%8]; // yellow
		}
		run = 48;
		sequence += "L";
		wait ();
	}
	void Li() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [5, i] = oldInfo [5, (i+2) % 8];
		}
		for (int i = 6; i < 9; i++) {
			sideColorList [2, i%8] = oldInfo [0, i%8]; // blue
			sideColorList [0, i%8] = oldInfo [3, i%8]; // white
		}
		for (int i = 6; i < 9; i++) {
			sideColorList [3, i%8] = oldInfo [1, ((i%8)+4)%8]; // green
			sideColorList [1, ((i%8)+4)%8] = oldInfo [2, i%8]; // yellow
		}
		run = 48;
		sequence += "Li";
		wait ();
	}
	void R() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [4, i - 6] = oldInfo [4, i % 8];
		}
		for (int i = 2; i < 5; i++) {
			sideColorList [2, i] = oldInfo [0, i]; // blue
			sideColorList [0, i] = oldInfo [3, i]; // white
		}
		for (int i = 2; i < 5; i++) {
			sideColorList [3, i] = oldInfo [1, (i+4)%8]; // green
			sideColorList [1, (i+4)%8] = oldInfo [2, i]; // yellow
		}
		run = 48;
		sequence += "R";
		wait ();
	}
	void Ri() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [4, i] = oldInfo [4, (i+2) % 8];
		}
		for (int i = 2; i < 5; i++) {
			sideColorList [3, i] = oldInfo [0, i]; // green
			sideColorList [0, i] = oldInfo [2, i]; // white
		}
		for (int i = 2; i < 5; i++) {
			sideColorList [2, i] = oldInfo [1, (i+4)%8]; // blue
			sideColorList [1, (i+4)%8] = oldInfo [3, i]; // yellow
		}
		run = 48;
		sequence += "Ri";
		wait ();
	}
	void U() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [1, i - 6] = oldInfo [1, i % 8];
		}
		for (int i = 0; i < 3; i++) {
			sideColorList [2, i] = oldInfo [4, i+2];
			sideColorList [3, i+4] = oldInfo [5, (i+6)%8];
			sideColorList [4, i+2] = oldInfo [3, i+4];
			sideColorList [5, (i+6)%8] = oldInfo [2, i];
		}
		//		sideColorList [2, 0] = oldInfo [4, 2]; //0W 1Y 2B 3G 4R 5O
		//		sideColorList [2, 1] = oldInfo [4, 3];
		//		sideColorList [2, 2] = oldInfo [4, 4];
		//		sideColorList [3, 4] = oldInfo [5, 6]; //0W 1Y 2B 3G 4R 5O
		//		sideColorList [3, 5] = oldInfo [5, 7];
		//		sideColorList [3, 6] = oldInfo [5, 0];
		//		sideColorList [4, 2] = oldInfo [3, 4]; //0W 1Y 2B 3G 4R 5O
		//		sideColorList [4, 3] = oldInfo [3, 5];
		//		sideColorList [4, 4] = oldInfo [3, 6];
		//		sideColorList [5, 6] = oldInfo [2, 0]; //0W 1Y 2B 3G 4R 5O
		//		sideColorList [5, 7] = oldInfo [2, 1];
		//		sideColorList [5, 0] = oldInfo [2, 2];
		run = 48;
		sequence += "U";
		wait ();
		for (int i = 0; i < 65535; i++) {

		}
	}
	void Ui() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [1, i] = oldInfo [1, (i+2) % 8];
		}
		for (int i = 0; i < 3; i++) {
			sideColorList [4, i+2] = oldInfo [2, i];
			sideColorList [5, (i + 6) % 8] = oldInfo [3, i + 4];
			sideColorList [3, i + 4] = oldInfo [4, i + 2];
			sideColorList [2, i] = oldInfo [5, (i + 6) % 8];
		}
		run = 48;
		sequence += "Ui";
		wait ();
	}
	void D() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 6; i < 14; i++) {
			sideColorList [0, i - 6] = oldInfo [0, i % 8];
		}
		for (int i = 4; i < 7; i++) {
			sideColorList [4, (i+2)%8] = oldInfo [2, i%8];
			sideColorList [5, (i + 6) % 8] = oldInfo [3, (i + 4)%8];
			sideColorList [3, (i + 4)%8] = oldInfo [4, (i + 2)%8];
			sideColorList [2, i%8] = oldInfo [5, (i + 6) % 8];
		}
		run = 48;
		sequence += "D";
		wait ();
	}
	void Di() {// blue towards, white down
		int [,] oldInfo = new int[6,8];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 8; j++) {
				oldInfo [i, j] = sideColorList [i, j];
			}
		}
		for (int i = 0; i < 8; i++) {
			sideColorList [0, i] = oldInfo [0, (i+2) % 8];
		}
		for (int i = 4; i < 7; i++) {
			sideColorList [2, i%8] = oldInfo [4, (i+2)%8];
			sideColorList [3, (i+4)%8] = oldInfo [5, (i+6)%8];
			sideColorList [4, (i+2)%8] = oldInfo [3, (i+4)%8];
			sideColorList [5, ((i+6)%8)] = oldInfo [2, i%8];
		}
		run = 48;
		sequence += "Di";
		wait ();
	}
	void test() {

	}
	void topWhiteEdge() {
		if (sideColorList [1, 1] == 0) { // Blue side
			switch (sideColorList [2, 1]) {
			case(4): // Blue tile
				F ();
				F ();
				blueEdge = true;
				break;
			case(5): // Green tile
				U ();
				U ();
				B ();
				B ();
				greenEdge = true;
				break;
			case(2): // Red tile
				Ui ();
				R ();
				R ();
				redEdge = true;
				break;
			case(3): // Orange tile
				U ();
				L ();
				L ();
				orangeEdge = true;
				break;
			default:
				break;
			}

		}
		if (sideColorList [1, 3] == 0) {
			switch (sideColorList [5, 7]) {
			case(3): // Orange tile
				L ();
				L ();
				orangeEdge = true;
				break;
			case(4): // Blue tile
				Ui ();
				F ();
				F ();
				blueEdge = true;
				break;
			case(5): // Green tile
				U ();
				B ();
				B ();
				greenEdge = true;
				break;
			case(2): // Red tile
				Ui ();
				Ui ();
				R ();
				R ();
				redEdge = true;
				break;
			default:
				break;
			}
		}
		if (sideColorList [1, 5] == 0) {
			switch (sideColorList [3, 5]) {
			case(5): // Green tile
				B ();
				B ();
				greenEdge = true;
				break;
			case(4): // Blue tile
				Ui ();
				Ui ();
				F ();
				F ();
				blueEdge = true;
				break;
			case(2): // Red tile
				U ();
				R ();
				R ();
				redEdge = true;
				break;
			case(3): // Orange tile
				Ui ();
				L ();
				L ();
				orangeEdge = true;
				break;
			default:
				break;
			}
		}
		if (sideColorList [1, 7] == 0) {
			switch (sideColorList [4, 3]) {
			case(2): // Red tile
				R ();
				R ();
				redEdge = true;
				break;
			case(3): // Orange tile
				U ();
				U ();
				L ();
				L ();
				orangeEdge = true;
				break;
			case(4): // Blue tile
				U ();
				F ();
				F ();
				blueEdge = true;
				break;
			case(5): // Green tile
				Ui ();
				B ();
				B ();
				greenEdge = true;
				break;
			default:
				break;
			}

		}
	}
	void edgeWhiteEdge() {
		if (sideColorList [2, 3] == 0) {
			R ();
			U ();
			Ri ();
			topWhiteEdge ();
		}
		if (sideColorList [2, 7] == 0) {
			Li ();
			U ();
			L ();
			topWhiteEdge ();
		}
		if (sideColorList [3, 3] == 0) {
			Ri ();
			U ();
			R ();
			topWhiteEdge ();
		}
		if (sideColorList [3, 7] == 0) {
			L ();
			U ();
			Li ();
			topWhiteEdge ();
		}
		if (sideColorList [4, 1] == 0) {
			Fi ();
			U ();
			F ();
			topWhiteEdge ();
		}
		if (sideColorList [4, 5] == 0) {
			B ();
			U ();
			Bi ();
			topWhiteEdge ();
		}
		if (sideColorList [5, 1] == 0) {
			F ();
			U ();
			Fi ();
			topWhiteEdge ();
		}
		if (sideColorList [5, 5] == 0) {
			Bi ();
			U ();
			B ();
			topWhiteEdge ();
		}
	}
	void outTopWhiteEdge() {
		if (sideColorList [2, 1] == 0) {
			F ();
			R ();
			U ();
			U ();
			Ri ();
			Fi ();
			topWhiteEdge ();
		}
		if (sideColorList [3, 5] == 0) {
			B ();
			L ();
			U ();
			U ();
			Li ();
			Bi ();
			topWhiteEdge ();
		}
		if (sideColorList [4, 3] == 0) {
			R ();
			B ();
			U ();
			U ();
			Bi ();
			Ri ();
			topWhiteEdge ();
		}
		if (sideColorList [5, 7] == 0) {
			L ();
			F ();
			U ();
			U ();
			Fi ();
			Li ();
			topWhiteEdge ();
		}
	}
	void botWhiteEdge () {
		if (sideColorList [2, 5] == 0) {
			F ();
			edgeWhiteEdge ();
		}
		if (sideColorList [3, 1] == 0) {
			B ();
			edgeWhiteEdge ();
		}
		if (sideColorList [4, 7] == 0) {
			R ();
			edgeWhiteEdge ();
		}
		if (sideColorList [5, 3] == 0) {
			L ();
			edgeWhiteEdge ();
		}
	}
	void downWhiteEdge() {
		if (sideColorList [2, 5] != 4 && sideColorList [0, 1] == 0) {
			F ();
			F ();
			topWhiteEdge ();
		}
		if (sideColorList [3, 1] != 5 && sideColorList [0, 5] == 0) {
			B();
			B();
			topWhiteEdge();
		}
		if (sideColorList [4, 7] != 2 && sideColorList [0, 3] == 0) {
			R ();
			R ();
			topWhiteEdge ();
		}
		if (sideColorList [5, 3] != 3 && sideColorList [0, 7] == 0) {
			L ();
			L ();
			topWhiteEdge ();
		}
	}
	void topOutWhiteCorner() {
		bool solveDetected = false;
		for (int i = 0; i < 4; i++) {
			if (sideColorList [4, 4] == 2 && sideColorList [1, 6] == 5 && sideColorList [3, 4] == 0) { // greenRed red
				B ();
				U ();
				Bi ();
				solveDetected = true;
				greenRed = true;
			}
			if (sideColorList [4, 4] == 0 && sideColorList [1, 6] == 2 && sideColorList [3, 4] == 5) { // greenRed green
				Ri ();
				Ui ();
				R ();
				solveDetected = true;
				greenRed = true;
			}
			if (sideColorList [3, 6] == 5 && sideColorList [5, 6] == 0 && sideColorList [1, 4] == 3) { // orangeGreen green
				L ();
				U ();
				Li ();
				solveDetected = true;
				orangeGreen = true;
			}
			if (sideColorList [3, 6] == 0 && sideColorList [5, 6] == 3 && sideColorList [1, 4] == 5) { // orangeGreen orange
				Bi ();
				Ui ();
				B ();
				solveDetected = true;
				orangeGreen = true;
			}
			if (sideColorList [2, 0] == 0 && sideColorList [5, 0] == 3 && sideColorList [1, 2] == 4) { // blueOrange orange
				F ();
				U ();
				Fi ();
				solveDetected = true;
				blueOrange = true;
			}
			if (sideColorList [2, 0] == 4 && sideColorList [5, 0] == 0 && sideColorList [1, 2] == 3) { // blueOrange blue
				Li ();
				Ui ();
				L ();
				solveDetected = true;
				blueOrange = true;
			}
			if (sideColorList [2, 2] == 4 && sideColorList [4, 2] == 0 && sideColorList [1, 0] == 2) { // redBlue blue
				R ();
				U ();
				Ri ();
				solveDetected = true;
				redBlue = true;
			}
			if (sideColorList [2, 2] == 0 && sideColorList [4, 2] == 2 && sideColorList [1, 0] == 4) { // redBlue red
				Fi ();
				Ui ();
				F ();
				solveDetected = true;
				redBlue = true;
			}
			U ();
		}
		if (!solveDetected) {
			sequence.Remove (sequence.Length - 4);
		}
	}
	void topUpWhiteCorner () {
		if (sideColorList [1, 0] == 0) {
			R ();
			Ui ();
			Ui ();
			Ri ();
			topOutWhiteCorner ();
		}
		if (sideColorList [1, 2] == 0) {
			F ();
			Ui ();
			Ui ();
			Fi ();
			topOutWhiteCorner ();
		}
		if (sideColorList [1, 4] == 0) {
			L ();
			Ui ();
			Ui ();
			Li ();
			topOutWhiteCorner ();
		}
		if (sideColorList [1, 6] == 0) {
			B ();
			Ui ();
			Ui ();
			Li ();
			topOutWhiteCorner ();
		}
	}
	void botDownWhiteCorner() {
		if (sideColorList [0, 0] == 0 && sideColorList [2, 6] != 4) {
			F ();
			U ();
			Fi ();
			topOutWhiteCorner ();
		}
		if (sideColorList [0, 2] == 0 && sideColorList [2, 4] != 4) {
			R ();
			U ();
			Ri ();
			topOutWhiteCorner ();
		}
		if (sideColorList [0, 4] == 0 && sideColorList [3, 2] != 5) {
			B ();
			U ();
			Bi ();
			topOutWhiteCorner ();
		}
		if (sideColorList [0, 6] == 0 && sideColorList [3, 0] != 5) {
			L ();
			U ();
			Li ();
			topOutWhiteCorner ();
		}
	}
	void botOutWhiteCorner () {
		if (sideColorList [2, 4] == 0) {
			Fi ();
			Ui ();
			F ();
			topOutWhiteCorner ();
		}
		if (sideColorList [2, 6] == 0) {
			F ();
			U ();
			Fi ();
			topOutWhiteCorner ();
		}
		if (sideColorList [3, 0] == 0) {
			Bi ();
			Ui ();
			B ();
			topOutWhiteCorner ();
		}
		if (sideColorList [3, 2] == 0) {
			B ();
			U ();
			Bi ();
			topOutWhiteCorner ();
		}
		if (sideColorList [4, 0] == 0) {
			R ();
			U ();
			Ri ();
			topOutWhiteCorner ();
		}
		if (sideColorList [4, 6] == 0) {
			Ri ();
			Ui ();
			R ();
			topOutWhiteCorner ();
		}
		if (sideColorList [5, 2] == 0) {
			Li ();
			Ui ();
			L ();
			topOutWhiteCorner ();
		}
		if (sideColorList [5, 4] == 0) {
			L ();
			U ();
			Li ();
			topOutWhiteCorner ();
		}
	}
	void outBotWhiteEdge() {
		if (sideColorList [2, 5] == 0) {
			F ();
			Li ();
			U ();
			L ();
			topOutWhiteCorner ();
		}
		if (sideColorList [3, 1] == 0) {
			B ();
			Ri ();
			U ();
			R ();
			topOutWhiteCorner ();
		}
		if (sideColorList [4, 7] == 0) {
			R ();
			Fi ();
			U ();
			F ();
			topOutWhiteCorner ();
		}
		if (sideColorList [5, 3] == 0) {
			L ();
			Bi ();
			U ();
			B ();
			topOutWhiteCorner ();
		}
	}
}