using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;

public class SocketsLoadCharacter : MonoBehaviour {

	private SocketIOComponent socket;

	private Text status_text;
	private Button login_button;

	private InputField username_text;
	private InputField password_text;

	// Use this for initialization
	void Start () {

		InitUI ();
		SetUsernameAndPassFromPreferencesIfPossible ();

		//same old, same old
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		//register for events via coordinated string keys
		//there are some defaults, like connect, open, close
		socket.On("connect", OnSocketConnect);
		socket.On("error", OnSocketError);
		socket.On("close", OnSocketClose);
		socket.On("login_success", LoginSuccess);

	}

	private void InitUI(){
		status_text = GameObject.Find ("SocketText").GetComponent<Text> ();
		status_text.text = "status : SEARCHING";

		login_button = GameObject.Find ("SocketButton").GetComponent<Button> ();
		login_button.onClick.AddListener(LoginToServer);


		username_text = GameObject.Find ("UsernameField").GetComponent<InputField> ();
		password_text = GameObject.Find ("PasswordField").GetComponent<InputField> ();
	}

	private void SetUsernameAndPassFromPreferencesIfPossible(){
		//http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
		if(PlayerPrefs.HasKey("username")){
			username_text.text = PlayerPrefs.GetString("username");
		};
		//storing a password like this is a terrible idea
		if(PlayerPrefs.HasKey("password")){
			password_text.text = PlayerPrefs.GetString("password");
		};
	}

	private void LoginToServer(){
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("username", username_text.text);
		json.AddField("password", password_text.text);
		socket.Emit ("client_login", json);
	}

	public void LoginSuccess(SocketIOEvent e){

		Debug.Log("LOGIN SUCCESS: " + e.name + " " + e.data);
		PlayerPrefs.SetString ("username", username_text.text);
		PlayerPrefs.SetString ("password", password_text.text);
	}

	//called by our socket event - mapped to a specific event in this case
	public void OnSocketConnect(SocketIOEvent e)
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
	//called by our socket event - mapped to a specific event in this case
	public void OnSocketError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] socket error: " + e.name + " " + e.data);
		status_text.text = "status : socket error";
	}
	//called by our socket event - mapped to a specific event in this case
	public void OnSocketClose(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] socket closed: " + e.name + " " + e.data);
		status_text.text = "status : closed";
	}

}



