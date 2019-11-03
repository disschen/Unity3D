using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mygame;

public class FirstController : MonoBehaviour, SceneController, UserAction {
	private int score = 0;

	public CrowFactory crow_fact;
	
	private bool playing_game = false;                                       //游戏中
	private bool game_over = false;                                          //游戏结束
	private bool game_start = false;

	private List <Crow> all_crows = new List<Crow>();
	private GameObject user;
	// Use this for initialization
	void Start () {
		Director director = Director.getInstance();     
		director.currentSceneController = this;  
		crow_fact = Singleton<CrowFactory>.Instance;
		LoadResources ();
	}
	
	// Update is called once per frame
	void Update () {
		if (game_start) {
			all_crows = crow_fact.GetCrow(); 
			LoadUser ();
			game_start = false;
		}
	}

	public void LoadResources()
	{
		LoadEgg ();
		LoadPlane ();
		LoadWall ();
		LoadObstacle ();
	}
	public void LoadEgg(){
		GameObject egg = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/egg.prefab", typeof(GameObject)),new Vector3(50,0,10) , Quaternion.identity) as GameObject;
	}
	public void LoadPlane(){
		for (int i = 0; i < 5; i ++) {
			for(int j = 0;j < 3;j ++){
				GameObject plane = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Plane.prefab", typeof(GameObject)),new Vector3(10*i,0,10*j) , Quaternion.identity) as GameObject;
			}
		}
		GameObject plane1 = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Plane.prefab", typeof(GameObject)),new Vector3(-10,0,10) , Quaternion.identity) as GameObject;
		GameObject plane2 = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Plane.prefab", typeof(GameObject)),new Vector3(50,0,10) , Quaternion.identity) as GameObject;
	}

	public void LoadWall(){
		for (int i = 0; i < 2; i ++) {
			for(int j =0;j < 11; j ++){
				GameObject wall = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall.prefab", typeof(GameObject)),new Vector3(5*j-5,0,30*i-5) , Quaternion.identity) as GameObject;
			}
		}
		for (int i = 0; i < 2; i ++) {
			for(int j =0;j < 7; j ++){
				if(j == 3) continue;
				GameObject wall = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall1.prefab", typeof(GameObject)),new Vector3(50*i-5,0,5*j-5) , Quaternion.Euler(0, 90, 0)) as GameObject;
			}
		}
	}

	public void LoadUser(){

		user = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/rooster.prefab", typeof(GameObject)),new Vector3(-10,0,10) , Quaternion.Euler(0, -90, 0)) as GameObject;
	}

	public void LoadObstacle(){
		GameObject wall = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall1.prefab", typeof(GameObject)),new Vector3(5,0,10) , Quaternion.Euler(0, 90, 0)) as GameObject;
		GameObject wall1 = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall1.prefab", typeof(GameObject)),new Vector3(15,0,0) , Quaternion.Euler(0, 90, 0)) as GameObject;
		GameObject wall2 = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall1.prefab", typeof(GameObject)),new Vector3(15,0,20) , Quaternion.Euler(0, 90, 0)) as GameObject;
		GameObject wall3 = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Wall1.prefab", typeof(GameObject)),new Vector3(30,0,10) , Quaternion.Euler(0, 0, 0)) as GameObject;
	}

	public void ReceiveMessage(string msg){
		if (msg == "game over") {
			Debug.Log ("game over right here");
			GameOver ();
		} else if (msg == "add score") {
			Debug.Log ("add score right here");
			Debug.Log (score);
			score ++;
		} else if (msg == "win") {
			GameOver();
		}
	}
	public int GetScore(){
		return score;
	}
	public void GameOver(){
		game_over = true;
	}

	public bool IfGameOver(){
		return game_over;
	}

	void DestroyAny(){
		Destroy (user);
		for (int i = 0; i < all_crows.Count; i ++)
			Destroy (all_crows [i].crow);
	}

	public void ReStart(){
		DestroyAny ();
		game_start = true;
		game_over = false;
		playing_game = false;
		score = 0;
	}
	public void BeginGame(){
		Debug.Log ("game start");
		game_start = true;
	}
}
