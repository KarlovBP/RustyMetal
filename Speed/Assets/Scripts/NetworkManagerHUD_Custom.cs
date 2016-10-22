using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkManagerHUD_Custom : MonoBehaviour {

	private NetworkManager manager;
	[HideInInspector]public float offsetY;
	public GameObject Scam;
	[SerializeField] public bool showGUI = true;
	public bool Load = false,setings_on = false;
	public Texture2D Loading,Menu,IconBorder,PodMenu,MatchName,Settings;
	public GUISkin gs;
	// Use this for initialization
	void Start () {
		Load = false;
		Scam = GameObject.Find ("SecondCamera");
		manager = GetComponent<NetworkManagerCustom>();

	}
		

	// Update is called once per frame
	void Update () {
	}
	public void OnPlayClick(){
		manager.StartMatchMaker ();
	}
	void OnGUI()
	{
		if (!showGUI)
			return;

		float ypos = 40 + offsetY;
		const float spacing = 24;

		if (Load && !ClientScene.ready) {
			GUI.DrawTexture(new Rect(Screen.width-200,Screen.height-70,200,70),Loading);
		}
		if (ClientScene.ready) {
			Scam.SetActive (false);
		}
		if (NetworkServer.active || manager.IsClientConnected ()) {
			if (GUI.Button (new Rect (10, 10, 100, 20), "Выйти")) {
				manager.StopHost ();
				Load = false;
				Scam.SetActive (true);
			}
		}
		if (!NetworkServer.active && !manager.IsClientConnected ()) {

			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Menu);
			if (GUI.Button (new Rect (Screen.width/15,Screen.height/40,Screen.width/6,Screen.height/14), "",gs.GetStyle("PlayButton"))) {
				manager.StartMatchMaker ();
			}
			if (GUI.Button (new Rect (Screen.width/4,Screen.height/40,Screen.width/6,Screen.height/14), "",gs.GetStyle("MagazButton"))) {
				//magaz
			}
			if (GUI.Button (new Rect (Screen.width/1.172f,Screen.height/19,Screen.width/35,Screen.height/20), "",gs.GetStyle("FriendsButton"))) {
				//friend
			}
			if (GUI.Button (new Rect (Screen.width/1.13f,Screen.height/19,Screen.width/35,Screen.height/20), "",gs.GetStyle("QuestButton"))) {
				//quest
			}
			if (GUI.Button (new Rect (Screen.width/1.09f,Screen.height/19,Screen.width/35,Screen.height/20), "",gs.GetStyle("SettingsButton"))) {
				
				setings_on = !setings_on;
			}

			GUI.DrawTexture (new Rect(Screen.width/1.057f,Screen.height/110,Screen.width/19,Screen.height/9),IconBorder);


			if (manager.matchMaker == null) {
				return;

			} else {
				if (manager.matchInfo == null) {
					if (manager.matches == null) {
						GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),PodMenu);
						if (GUI.Button (new Rect (Screen.width/15,Screen.height/7.5f, Screen.width/10,Screen.height/20), "",gs.GetStyle("MatchButton"))) {
							manager.matchMaker.CreateMatch (manager.matchName+"/"+manager.onlineScene, manager.matchSize, true, "", manager.OnMatchCreate);
						}

						if (GUI.Button (new Rect (Screen.width/25, Screen.height/2, Screen.width/4,Screen.height/4), "",gs.GetStyle("FootballMap"))) {
							manager.onlineScene = "stadium";
						}
						if (GUI.Button (new Rect (Screen.width/3, Screen.height/2.03f, Screen.width/4,Screen.height/4), "",gs.GetStyle("JumpMap"))) {
							manager.onlineScene = "Jump";
						}
						if (GUI.Button (new Rect (Screen.width/1.4f, Screen.height/2.03f, Screen.width/4,Screen.height/4), "",gs.GetStyle("RaceMap"))) {
							manager.onlineScene = "desert";
						}


						GUI.DrawTexture (new Rect (Screen.width/2 - MatchName.width, Screen.height/5,Screen.width/6,Screen.height/14), MatchName);
						manager.matchName = GUI.TextField (new Rect (Screen.width/2.45f, Screen.height/3.5f,Screen.width/4,Screen.height/15), manager.matchName,20);


						if (GUI.Button (new Rect (Screen.width/5,Screen.height/7.5f, Screen.width/10,Screen.height/20), "",gs.GetStyle("FindMatchButton"))) {
							manager.matchMaker.ListMatches (0, 20, "", manager.OnMatchList);
						}

					} else {
						foreach (var match in manager.matches) {
							if (GUI.Button (new Rect (20, ypos, 400, 20), "Присоеденится к матчу :" + match.name +"    "+ match.currentSize  +"/"+ manager.matchSize)) {
								Load = true;
								manager.matchName = match.name;
								string[] splitMessage = match.name.Split ('/');
								manager.onlineScene=splitMessage [1];
								manager.matchMaker.JoinMatch (match.networkId, "", manager.OnMatchJoined);
							}
							ypos += spacing;
						}
					}
					if (GUI.Button(new Rect(0, 0, 100, 20), "Назад"))
					{
						manager.StopMatchMaker();
					}
						
				}
				if(setings_on)
					GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Settings);

			}
		}
	}
}
