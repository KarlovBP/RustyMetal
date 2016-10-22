using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooting1 : NetworkBehaviour {

	public float minDamage, maxDamage,DealDamage;
	public Transform GUN,FlashPos,FlashPref;
	public Transform Sparks;
	[SyncVar] RaycastHit hitFX;
	public float ammo = 30;
	public bool canShoot = true;
	public AudioClip reloadSound; // звук перезарядки
	// Use this for initialization
	void Start () {
		minDamage = 1;
		maxDamage = 5;
		canShoot = true;
	}

	void OnGUI () {
		if(isLocalPlayer)
			GUI.Label (new Rect(Screen.width-200,Screen.height-50,50,50),"Патрохи " + ammo);
	}

	void SetDamage(){
		DealDamage = Random.Range (minDamage, maxDamage);
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		if (Input.GetMouseButtonDown(0) & canShoot==true) {
			canShoot = false;
			Shoot();

		}
		Debug.DrawRay (GUN.position, GUN.forward * 10f);

	}



	void Shoot(){//SHOOTING
		ammo -= 1;
		CmdEffect ();
		Ray ray = new Ray (GUN.position, GUN.forward * 10f);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
		{				
			hitFX = hit;
			CmdSparks ();
			if (hit.transform.tag == "Player")
			{
				SetDamage ();
				CmdShoot (hit.transform.root.GetComponent<NetworkIdentity> ().netId, DealDamage);
			}
		}
		if (ammo == 0) { //RELOAD
			canShoot = false;
			StartCoroutine (delayReload ());
		} else 
		{
			StartCoroutine (delayShoot ());
		}


	}
	IEnumerator delayReload() {
		GetComponent<AudioSource>().PlayOneShot(reloadSound);
		yield return new WaitForSeconds(reloadSound.length + 0.5f); 
		ammo += 150;
		canShoot = true;
	}
	IEnumerator delayShoot() {
		yield return new WaitForSeconds(0.2f);
		// если зажата левая кнопка мышиS
		if (Input.GetMouseButton(0))
		{
			// стреляем
			Shoot();
		}else{ // если не зажата левая кнопка мыши...
			// включаем триггер (стрелять можно)
			canShoot = true;

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
	[Command]
	void CmdSparks(){
		RpcSparks ();
	}
	[ClientRpc]
	void RpcSparks(){
		Instantiate (Sparks,hitFX.point,Quaternion.FromToRotation(Vector3.up, hitFX.normal));
	}


}
