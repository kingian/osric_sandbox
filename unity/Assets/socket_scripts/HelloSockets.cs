using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;

public class HelloSockets : MonoBehaviour {

	private SocketIOComponent socket;

	private Text socket_text;
	private Button socket_button;


	// Use this for initialization
	void Start () {

		InitUI ();

		//same old, same old
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		//register for events via coordinated string keys
		//there are some defaults, like connect, open, close
		socket.On("connect", ConnectedToServer);

	}

	private void InitUI(){
		socket_text = GameObject.Find ("SocketText").GetComponent<Text> ();
		socket_text.text = "status : SEARCHING";

		socket_button = GameObject.Find ("SocketButton").GetComponent<Button> ();
		socket_button.onClick.AddListener(SocketButtonWasClicked);
	}

	private void SocketButtonWasClicked(){
		Debug.Log ("i clicked the button!");

		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("data", "Hey, someone touched my button!!!!");
		socket.Emit ("update_from_client", json);
	}

	//called by our socket event - mapped to a specific event in this case
	public void ConnectedToServer(SocketIOEvent e)
	{
		//cool. we connected to the server and got the event data
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
		//lets send a message to the server and let know who's boss
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("data", "I'm unity, bitch");
		socket.Emit ("update_from_client", json);
	}

}



