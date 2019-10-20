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
	
	public interface IUFOMove{
		void moveTo(Vector3 pos, Vector3 des, float speed);
	}

	public interface UserAction {
		void Hit(Vector3 pos);
		int GetScore();
		void GameOver();
		void ReStart();
		void BeginGame();
		void FlyUFO();
	}
	
	
	public class UFO {
		public GameObject ufo;
		public int type;
		public int speed;
		public int score = 1;                               //射击此飞碟得分
		public Moveable ms;
		
		// 增加physicMoveable实例pt
		public physicMoveable pt;
		
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
			
			// 添加physicMoveable组件
			pt = ufo.AddComponent(typeof(physicMoveable)) as physicMoveable;
		}
		
		public void Fly(){
			int fy0 = Random.Range(-10,10);
			ufo.transform.position = new Vector3(0,fy0,0);
			int fy = Random.Range(-10,10);
			Vector3 des = new Vector3(20,fy,0);
			Vector3 start = ufo.transform.position;
			
			// 选择那个就取消哪个的注释
			pt.moveTo(start,des,speed);
			// ms.setDestination(des,speed);
		}
	}


}
