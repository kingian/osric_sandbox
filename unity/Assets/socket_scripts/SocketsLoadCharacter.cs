using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;

public class SocketsLoadCharacter : MonoBehaviour {

	private SocketIOComponent socket;

	private Text status_text;
	private Button login_button;

	private Text username_text;
	private Text password_text;

	// Use this for initialization
	void Start () {

		InitUI ();
		SetUsernameAndPassFromPreferencesIfPossible ();

		//same old, same old
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		//register for events via coordinated string keys
		//there are some defaults, like connect, open, close
		socket.On("connect", ConnectedToServer);

	}

	private void InitUI(){
		status_text = GameObject.Find ("SocketText").GetComponent<Text> ();
		status_text.text = "status : SEARCHING";

		login_button = GameObject.Find ("SocketButton").GetComponent<Button> ();
		login_button.onClick.AddListener(LoginToServer);


		username_text = GameObject.Find ("UsernameField").GetComponent<Text> ();
		password_text = GameObject.Find ("PasswordField").GetComponent<Text> ();
	}

	private void SetUsernameAndPassFromPreferencesIfPossible(){
		//http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
	}

	private void LoginToServer(){
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("data", "Hey, someone touched my button!!!!");
		json.AddField("username", "username");
		json.AddField("password", "Hey, someone touched my button!!!!");
		socket.Emit ("update_from_client", json);
	}

	//called by our socket event - mapped to a specific event in this case
	public void ConnectedToServer(SocketIOEvent e)
	{
		//cool. we connected to the server and got the event data
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
		//update the UI
		status_text.text = "status : CONNECTED!!!!";
		//lets send a message to the server and let know who's boss
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("data", "I'm unity, bitch");
		socket.Emit ("update_from_client", json);
	}

}



