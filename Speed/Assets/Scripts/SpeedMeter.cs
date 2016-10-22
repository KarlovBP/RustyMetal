using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

namespace UnityStandardAssets.Vehicles.Car
{
public class SpeedMeter : NetworkBehaviour {
		[HideInInspector]public CarController cc;
		public int speed;
		public Texture Speedometr,SpeedometrPoint;
	// Use this for initialization
	void Start () {
			cc = gameObject.GetComponent<CarController> ();
	}
	
	// Update is called once per frame
		void OnGUI(){
			if (isLocalPlayer) {
				speed = Mathf.RoundToInt (cc.CurrentSpeed);
				GUI.Label (new Rect (Screen.width-300, Screen.height - 200, 250, 50), speed + " MPH");
				float speedFactor = cc.CurrentSpeed / cc.MaxSpeed;
				float rotationAngle;
				rotationAngle = Mathf.Lerp (0, 180, speedFactor);
				GUI.DrawTexture(new Rect(Screen.width - 300,Screen.height-150,300,150),Speedometr);
				GUIUtility.RotateAroundPivot(rotationAngle,new Vector2(Screen.width-150,Screen.height));
				GUI.DrawTexture(new Rect(Screen.width - 300,Screen.height-150,300,300),SpeedometrPoint);

			}
		}
	}
}
