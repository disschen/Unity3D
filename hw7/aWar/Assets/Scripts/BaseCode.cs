using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mygame;

namespace mygame {
	
	public class Director : System.Object {
		private static Director _instance;
		public SceneController currentSceneController { get; set; }
		
		public static Director getInstance() {
			if (_instance == null) {
				_instance = new Director ();
			}
			return _instance;
		}
	}
	
	public interface SceneController {
		void LoadResources ();
	}

	public interface UserAction {
		void ReceiveMessage(string msg);
		void GameOver();
		void ReStart();
		void BeginGame();
		int GetScore();
		bool IfGameOver();
	}

	public class Crow{
		public GameObject crow;
		public Vector3 startPoint;
		public Patrolable pt;

		public Crow(float x,float y){

			startPoint = new Vector3 (10 * x, 0.5f, 10 * y);
			crow = Object.Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/crow.prefab", typeof(GameObject)),startPoint , Quaternion.identity) as GameObject;
			//crow = Object.Instantiate(Resources.Load("Assets/Prefabs/crow"),new Vector3(0,0,0), Quaternion.identity);
			pt = crow.AddComponent(typeof(Patrolable)) as Patrolable;
			if (x >= 3.0f) {
				pt.SetSpeed(6);
			}
		}
	}
}