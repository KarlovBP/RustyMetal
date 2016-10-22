using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ChangeCar : NetworkBehaviour {
	public GameObject carPosition,MainCamera;
	public List<GameObject> Cars = new List<GameObject> ();
	public int CarsCount;
	[SyncVar] int CarID=0;
	public bool Changecar;
	private GameObject NM;
	// Use this for initialization
	void Start () {
		NM = GameObject.Find ("Network Manager");
	}
	void Change(){
		if (CarID < CarsCount && CarID>0) {
			Instantiate (Cars [CarID], carPosition.transform.position, carPosition.transform.rotation);
			Destroy (carPosition);
		} else if (CarID == CarsCount) {
			CarID = 0;
			Instantiate (Cars [CarID], carPosition.transform.position, carPosition.transform.rotation);
			Destroy (carPosition);
		}  
		if (CarID < 0) {
			CarID = CarsCount - 1;
			Instantiate (Cars [CarID], carPosition.transform.position, carPosition.transform.rotation);
			Destroy (carPosition);
		}
	}
	// Update is called once per frame
	void Update(){
		//carPosition = GameObject.FindGameObjectWithTag ("Car");
		//CarsCount = Cars.Count;
	}
	void OnGUI () {
	if (NM.GetComponent<Login> ().conn ) {
			if(!Changecar)
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height - 100, 100, 50), "Машина")) {
				Changecar = true;
			}
			if (Changecar) {
				if (GUI.Button (new Rect (Screen.width / 1.5f, Screen.height - 100, 50, 50), ">")) {
					CarID++;
					Change ();
				}
				if (GUI.Button (new Rect (Screen.width / 3f, Screen.height - 100, 50, 50), "<")) {
					CarID--;
					Change ();
				}
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height - 100, 100, 50), "Выбрать")) {
					
					Changecar = false;
				}
			}

		}
	}
}
