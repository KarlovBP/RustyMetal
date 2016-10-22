using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ChildTransform : NetworkBehaviour {
	[SyncVar] private Vector3 syncPosition;



	void Start () {
		syncPosition = transform.position;
	}
	void Update(){
		if (isClient) {
			transform.position = Vector3.Lerp (transform.position, syncPosition, 5 * Time.deltaTime);
		}
			
		if (isServer) {
			syncPosition = transform.position;	
		}
	}
	

}
