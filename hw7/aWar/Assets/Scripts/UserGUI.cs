using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	GUIStyle bold_style = new GUIStyle();
	GUIStyle score_style = new GUIStyle();
	GUIStyle text_style = new GUIStyle();
	GUIStyle over_style = new GUIStyle();

	private bool game_start = false;       //游戏开始

	// Use this for initialization
	void Start ()
	{
		action = Director.getInstance().currentSceneController as UserAction;
	}

	void Update(){
		if(action == null)
			action = Director.getInstance().currentSceneController as UserAction;
	}
	
	// Update is called once per frame
	void OnGUI ()
	{
		bold_style.normal.textColor = new Color(1, 0, 0);
		bold_style.fontSize = 16;
		text_style.normal.textColor = new Color(0,0,0, 1);
		text_style.fontSize = 16;
		score_style.normal.textColor = new Color(1,0,1,1);
		score_style.fontSize = 16;
		over_style.normal.textColor = new Color(1, 0, 0);
		over_style.fontSize = 25;
		
		if (!game_start) {
			GUI.Label(new Rect(Screen.width / 2-200, Screen.height / 2 - 320, 400, 100), "你的孩子被乌鸦抓走了！快去拯救它吧！", text_style);
			if (GUI.Button(new Rect(Screen.width / 2-100 , Screen.height / 2-250, 100, 50), "游戏开始"))
			{
				
				Debug.Log(game_start);
				game_start = true;
				action.BeginGame();
			}
		} else {

			
			GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
			GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);

			//游戏结束
			if (action.IfGameOver())
			{
				GUI.Label(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
				if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 150, 100, 50), "重新开始"))
				{
					action.ReStart();
					return;
				}
				action.GameOver();
			}
		}
	}
}
