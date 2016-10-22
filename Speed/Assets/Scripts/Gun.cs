using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Gun : NetworkBehaviour {
	public float minDamage,maxDamage,DealDamage;
	public Transform GUN,FlashPos,FlashPref,Sparks;
	// Use this for initialization
	void Start () {
		minDamage = 1;
		maxDamage = 5;
	}

	void SetDamage(){
		DealDamage = Random.Range (minDamage, maxDamage);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		Debug.DrawRay (GUN.position,GUN.forward*10f);
		if (Input.GetMouseButtonDown (0)) {
			CmdEffect ();
			Ray ray = new Ray (GUN.position, GUN.forward * 10f);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if (hit.transform.tag == "Player") {
					SetDamage ();
					CmdShoot (hit.transform.root.GetComponent<NetworkIdentity> ().netId, DealDamage);
				}
			}
		}
	}
	[Command]
	void CmdShoot(NetworkInstanceId id,float dmg){
		NetworkServer.FindLocalObject (id).GetComponent<PlayerInfo> ().GetDamage (dmg);
	}
	[Command]
	void CmdEffect(){
		Rpceffect ();
	}
	[ClientRpc]
	void Rpceffect(){
		Transform tr = Instantiate (FlashPref,FlashPos.position,FlashPos.rotation)as Transform;
		tr.parent = transform;
	}
}
