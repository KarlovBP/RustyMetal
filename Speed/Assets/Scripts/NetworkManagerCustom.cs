using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManagerCustom : NetworkManager {

	[SerializeField] Vector3 playerSpawnPos;
	public List<GameObject> playerSpawnPoints = new List<GameObject>();
	public int ClientConnected = 0;
	public bool inGame;
	public int IdToSpawn;

	// Use this for initialization
	void Start () {

	}

	void Update(){
		if (ClientConnected == 2) {
			playerPrefab = spawnPrefabs [1];
		}
		if (ClientConnected == 3) {
			playerPrefab = spawnPrefabs [0];
		}
		if (ClientConnected == 4) {
			playerPrefab = spawnPrefabs [1];
		}

	}
	public override void OnServerConnect(NetworkConnection conn){
		if (ClientConnected > 0) { 
			ClientConnected += 1;
			playerSpawnPoints.Add (GameObject.Find("SpawnPoint"+ClientConnected));
		}
		base.OnServerConnect (conn);


	}

	public override void OnClientConnect(NetworkConnection conn){
		if (ClientConnected == 0) { 
			ClientConnected += 1;
			inGame = true;
			playerSpawnPoints.Add (GameObject.Find("SpawnPoint"+ClientConnected));
		}
		base.OnClientConnect (conn);

	}
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		ClientConnected -= 1;
		playerSpawnPoints.RemoveAt (ClientConnected);
		base.OnServerDisconnect(conn);
	}

	public override void OnStopServer ()
	{
		ClientConnected = 0;
		playerSpawnPoints.Clear ();
		base.OnStopServer ();
	}


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{	

			playerSpawnPos = playerSpawnPoints [ClientConnected - 1].transform.position;
			var player = (GameObject)GameObject.Instantiate (playerPrefab, playerSpawnPos, Quaternion.identity);
			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		}
		
}



