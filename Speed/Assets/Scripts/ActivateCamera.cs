using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ActivateCamera : NetworkBehaviour {
	public GameObject CarCam;
	public GameObject CarName;
	public Transform carTransform;
	[SyncVar]public string Synccarname;
	[SyncVar] private Vector3 syncPosition;
	public float movementLerpRate = 15;


	[SyncVar] private Quaternion syncRotation;
	public float rotationLerpRate = 15;

	// Use this for initialization
	void Start () {
		carTransform = GetComponent<Transform> ();
		if (!isLocalPlayer) {
			CarCam.SetActive (false);
		}

		if (isLocalPlayer) {
			CarName.SetActive (false);
		}
		}
	
	private void FixedUpdate(){
	
		LerpPosition ();
		LerpRotation ();
		CarN ();
		SendPosition ();
		//SendName ();
	
	}

	private void CarN(){
	
		if (!isLocalPlayer) {
		
			CarName.GetComponentInChildren<TextMesh>().text = Synccarname;

		}

	}
	private void LerpPosition(){
	
		if (!isLocalPlayer) {
			carTransform.position = Vector3.Lerp (carTransform.position,syncPosition,Time.deltaTime * movementLerpRate);
		}
	
	}

	private void LerpRotation(){
	
		if (!isLocalPlayer) {
			carTransform.rotation = Quaternion.Lerp (carTransform.rotation, syncRotation, Time.deltaTime * rotationLerpRate);
		}
	
	}

	[Command]
	private void CmdProvidePosToServer(Vector3 Position, Quaternion rot){
	
		syncPosition = Position;
		syncRotation = rot;

	
	}

	[ClientCallback]
	private void SendPosition(){
	
		if (isLocalPlayer)
			CmdProvidePosToServer (carTransform.position,carTransform.rotation);
	
	}

	/*[Command]
	private void CmdProvideNameToServer(string cn){

		Synccarname = cn;


	}*/

	/*[ClientCallback]
	private void SendName(){

		if (isLocalPlayer)
			CmdProvideNameToServer (GameObject.Find("Network Manager").GetComponent<Login>().Username);

	}*/


}
