using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Pointer : NetworkBehaviour {
	private GameObject Flag;
	public GameObject pointer;
	private GameObject Base1,Base2;
	private Vector3 dir;
	private PlayerInfo PI;
	// Use this for initialization
	void Start () {
		PI = gameObject.GetComponentInParent<PlayerInfo> ();
		Flag = GameObject.Find ("Flag");
		Base1 = GameObject.Find ("Base1Pos");
		Base2 = GameObject.Find ("Base2Pos");
		if(isLocalPlayer && GameObject.Find("Network Manager").GetComponent<NetworkManagerCustom>().onlineScene == "desert")
		pointer.SetActive(true);

	}
	public void ChangePointer(int team,bool have_flag){
		if (GameObject.Find ("Network Manager").GetComponent<NetworkManagerCustom> ().onlineScene != "desert")
			return;


		if (have_flag && team==1) {
			dir = Base1.transform.position - transform.position;
			Quaternion LookRot = Quaternion.LookRotation (dir);
			Vector3 rotation = Quaternion.Lerp (pointer.transform.rotation, LookRot, Time.deltaTime * 20f).eulerAngles;
			pointer.transform.rotation = Quaternion.Euler (-17f, rotation.y, 0f);
		}
		if (have_flag && team==2) {
			dir = Base2.transform.position - transform.position;
			Quaternion LookRot = Quaternion.LookRotation (dir);
			Vector3 rotation = Quaternion.Lerp (pointer.transform.rotation, LookRot, Time.deltaTime * 20f).eulerAngles;
			pointer.transform.rotation = Quaternion.Euler (-17f, rotation.y, 0f);
		}
		if (!have_flag) {
			dir = Flag.transform.position - transform.position;
			Quaternion LookRot = Quaternion.LookRotation (dir);
			Vector3 rotation = Quaternion.Lerp (pointer.transform.rotation, LookRot, Time.deltaTime * 20f).eulerAngles;
			pointer.transform.rotation = Quaternion.Euler (-17f, rotation.y, 0f);
		}
	}
	// Update is called once per frame
	void Update () {
		ChangePointer(PI.Team,PI.haveflag);
	}
}
