using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine.Networking;

public class Login : NetworkBehaviour {

	public string serverAddress = "192.168.0.104";
	public int serverPort = 32211;
	public String Username;
	public String Password;
	public bool conn;

	private TcpClient _client;
	private NetworkStream _stream;
	private Thread _thread;

	private byte[] _buffer = new byte[1024];
	private string receiveMsg = " ";
	private bool isConnected = false;
	private Chat c;
	private NetworkManagerHUD_Custom HUD;

	void Start() {

		SetupConnection();
		c = gameObject.GetComponent<Chat> ();
		HUD = GameObject.Find ("Network Manager").GetComponent<NetworkManagerHUD_Custom> ();

	}


	void OnApplicationQuit() {
		CloseConnection();
	}

	private void SetupConnection() {
		if(!conn)
		try {
			_thread = new Thread(ReceiveData);
			_thread.IsBackground = true;
			_client = new TcpClient(serverAddress, serverPort);
			_stream = _client.GetStream();
			_thread.Start();
			isConnected = true;
		} catch (Exception e) {
			CloseConnection ();
			Debug.Log(e.ToString());
		}
	}

	private void ReceiveData() {
		if(!isConnected)
			return;

		int numberOfBytesRead = 0;
		while(isConnected && _stream.CanRead) {
			try {
				numberOfBytesRead = _stream.Read(_buffer, 0, _buffer.Length);
				receiveMsg = Encoding.ASCII.GetString(_buffer, 0, numberOfBytesRead);
				_stream.Flush();
				LogIn();
				AddChatMessage();
				receiveMsg = "";
			} catch (Exception e) {
				Debug.Log(e.ToString());
				CloseConnection ();
			}
		}
	}

	private void LogIn(){

		if (receiveMsg == "+") {
			Debug.Log ("Вход выполнен успешно");
			conn = true;

		}
		else if(receiveMsg == "-")
			Debug.Log ("Вход не выполнен");

	}

	private void AddChatMessage(){
		string[] splitMessage = receiveMsg.Split ('/');
		if (splitMessage [0] == "0x000") {
			
			c.ChatHistory.Add(splitMessage[1]+splitMessage[2]+"\n");
			c.sp.y = Mathf.Infinity;
		}
	}

	private void CloseConnection() {
		if(isConnected) {
			_thread.Interrupt();
			_stream.Close();
			_client.Close();
			isConnected = false;
			receiveMsg = "";
		}
	}

	void OnGUI(){
		if (!conn) {
			Username = GUI.TextField (new Rect (10, 10, 150, 25), Username).ToString ();
			Password = GUI.PasswordField (new Rect (10, 45, 150, 25), Password, '*').ToString ();
			if (GUI.Button (new Rect (10, 100, 150, 25), "LOGIN"))
				SendData ("0x000/" + Username + "/" + Password);
		} else {
			HUD.showGUI = true;
		}
	}
		
	private void SendData(string msgToSend) {
		byte[] bytesToSend = Encoding.ASCII.GetBytes(msgToSend);
		if(_stream.CanWrite)
			_stream.Write(bytesToSend, 0, bytesToSend.Length);
	}
}
