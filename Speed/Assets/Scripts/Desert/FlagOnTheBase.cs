using UnityEngine;
using System.Collections;

public class FlagOnTheBase : MonoBehaviour {
	public int team;
	public GameObject Flag;

	// Use this for initialization
	void Start () {
		Flag = GameObject.Find ("Flag");
	}
	public void OnTriggerEnter(Collider other){
		PlayerInfo PI = other.gameObject.GetComponentInParent<PlayerInfo> ();
		if (PI.Team == team && PI.haveflag == true ) {
			
			Flag.GetComponent<FlagCatch> ().DestroyFlag ();         
			PI.haveflag = false;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
