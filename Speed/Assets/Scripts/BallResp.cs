using UnityEngine;
using System.Collections;

public class BallResp : MonoBehaviour {
	public GameObject BallRespawnPoint;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<Rigidbody> ().constraints=RigidbodyConstraints.FreezeAll;
			other.GetComponent<Rigidbody> ().constraints=RigidbodyConstraints.None;
			other.transform.position = BallRespawnPoint.transform.position;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
