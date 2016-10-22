using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FlagCatch : MonoBehaviour {
	private Vector3 Respawn;
	// Use this for initialization
	void Start () {
		Respawn = transform.position;

	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			gameObject.transform.parent = other.transform.root;
 			gameObject.GetComponentInParent<PlayerInfo> ().haveflag = true;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}

	public void DropFlag(){
		gameObject.transform.parent = null;
		transform.localScale = new Vector3 (1f, 1f, 1f);
		gameObject.GetComponent<BoxCollider> ().enabled = true;
	}
	public void DestroyFlag(){
		gameObject.transform.parent = null;
		transform.position = Respawn;
		transform.localScale = new Vector3 (1f, 1f, 1f);
		gameObject.GetComponent<BoxCollider> ().enabled = true;
		//очки команды +1 ;
	}
	// Update is called	 once per frame
	void Update () {
		
	}
}
