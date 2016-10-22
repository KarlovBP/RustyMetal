using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	public Vector3 RespPosition;
	// Use this for initialization
	void Start () {
		RespPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			transform.position = RespPosition;
		}
	}

	public void Resp_dead(){
		transform.position = RespPosition;
	}
}
