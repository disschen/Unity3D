using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		void Hit(Vector3 pos);
		int GetScore();
		void GameOver();
		void ReStart();
		void BeginGame();
		void FlyUFO();
	}

	/*-----------------------------------Moveable------------------------------------------*/
	public class Moveable: MonoBehaviour {
		
		int move_speed = 1;

		// change frequently
		int moving_status;	// 0->not moving, 1->moving to middle, 2->moving to dest
		Vector3 dest;
		Vector3 middle;

		void Update() {
			
			if (moving_status == 1) {
				// Debug.Log(transform.position);
				transform.position = Vector3.MoveTowards (transform.position, dest, move_speed * Time.deltaTime);
				if (transform.position == dest) {
					
					moving_status = 0;
				}
			}
		}
		public void setDestination(Vector3 _dest,int speed) {
			move_speed = speed;
			dest = _dest;
			moving_status = 1;
		}

		public void reset() {
			moving_status = 0;
		}
	}
	
	public class UFO {
		public GameObject ufo;
		public int type;
		public int speed;
		public int score = 1;                               //射击此飞碟得分
		readonly Moveable ms;
		
		
		public UFO(int ufo_type){
			
			type = ufo_type;
			// ufo.transform.scale = scale;
			Debug.Log("right here UFO 1");
			if (type == 1) {
				speed = 5;
				ufo = Object.Instantiate (Resources.Load ("Prefabs/disk1", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            } else if (type == 2)  {
				speed = 8;
				score = 2;
                ufo = Object.Instantiate (Resources.Load ("Prefabs/disk2", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            } else {
				speed = 10;
				score = 3;
                ufo = Object.Instantiate (Resources.Load ("Prefabs/disk3", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            }
			ms = ufo.AddComponent(typeof(Moveable)) as Moveable;
			int fy = Random.Range(-10,10);
			ufo.transform.position = new Vector3(0,fy,0);
            // direction = new Vector3(0,0,0);
		}
		
		public void Fly(){
			int fy = Random.Range(-10,10);
			ms.setDestination(new Vector3(20,fy,0),speed);
		}
	}


}
