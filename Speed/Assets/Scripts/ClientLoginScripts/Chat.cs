using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class Chat : MonoBehaviour {
	public Login log;
	public List<string> ChatHistory = new List<string>();
	public String chatmessage;
	public Vector2 sp = Vector2.zero;
	public Rect chatRect;
	// Use this for initialization
	void Start () {
		log = gameObject.GetComponent<Login> ();

	}
	
	// Update is called once per frame
		void OnGUI(){
		chatRect = new Rect(2,Screen.height*0.6f,Screen.width/2.5f,Screen.height*0.4f);
		if (log.conn) {
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

			if (GUI.Button (new Rect ( 260, Screen.height - 30, 50, 25), "send")) {
				log.SendMessage ("SendData", "0x002/" + log.Username + "/" + chatmessage);
				chatmessage = String.Empty;
			}

			chatmessage = GUI.TextField (new Rect (10, Screen.height - 30, 250, 25), chatmessage).ToString ();
		}
	}

	public void Update(){
		
	}
}
