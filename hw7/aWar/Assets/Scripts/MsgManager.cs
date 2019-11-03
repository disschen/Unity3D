using UnityEngine;
using System.Collections;
using mygame;

public class MsgManager : MonoBehaviour {
	private UserAction action;
	// Use this for initialization
	void Start () {
		action = Director.getInstance().currentSceneController as UserAction;
	}

	void Update(){
		if(action == null)
			action = Director.getInstance().currentSceneController as UserAction;
	}
	
	public void SendMsg(string msg, string from, string to){
		if (to == "FC") {
			Debug.Log ("FC");
			action.ReceiveMessage(msg);
		}
	}
	// Update is called once per frame
}
