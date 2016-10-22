using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.Networking;

public class inGameChat : NetworkBehaviour {
	public Login log;
	public List<string> ChatHistory = new List<string>();
	public List<GameObject> Players = new List<GameObject>();
	public String chatmessage;
	public Vector2 sp = Vector2.zero;
	public Rect chatRect;
	// Use this for initialization
	void Start () {
		log = GameObject.Find("Network Manager").GetComponent<Login> ();
	}
	void OnGUI(){
		
			chatRect = new Rect (2, Screen.height * 0.6f, Screen.width / 2.5f, Screen.height * 0.4f);
			GUILayout.BeginArea (chatRect);
			sp = GUILayout.BeginScrollView (sp);
			GUILayout.Space (5);
			GUILayout.BeginVertical ();
			foreach (string c in ChatHistory) {
				GUILayout.BeginHorizontal ();
				GUILayout.Space (5);
				GUILayout.Label (c);
				GUILayout.EndHorizontal ();
			}
			GUILayout.EndVertical ();
			GUILayout.EndScrollView ();
			GUILayout.EndArea ();

			if (GUI.Button (new Rect (260, Screen.height - 30, 50, 25), "send")) {
			// "User1" + " : " + chatmessage;

				chatmessage = String.Empty;
			}

			chatmessage = GUI.TextField (new Rect (10, Screen.height - 30, 250, 25), chatmessage).ToString ();

	}

}
