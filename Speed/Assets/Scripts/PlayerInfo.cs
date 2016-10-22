using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerInfo :NetworkBehaviour {
	[SyncVar] public float health=100;
	public int Team;
	public bool haveflag=false;
	// Use this for initialization
	void Start () {
		transform.name = "Car"+GetComponent<NetworkIdentity>().netId.ToString();
	}
	public void GetDamage(float dmg){
		health -= dmg;
	}

	void Update(){
		if (health <= 0) {
			if(haveflag)
			gameObject.GetComponentInChildren<FlagCatch> ().DropFlag ();
			gameObject.GetComponent<Respawn> ().Resp_dead ();
			health = 100;
			haveflag = false;
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(isLocalPlayer)
		GUI.Label (new Rect(Screen.width-100,Screen.height-50,50,50),"Health : " + health);
	}
}
