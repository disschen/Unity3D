using UnityEngine;
using System.Collections;
using mygame;

public class Patrolable : MonoBehaviour {
	public float domainWidth;
	public int pointNum;
	public int reverse;
	public int curPoint;
	public int nextPoint;
	public int speed;
	public bool findEnemy;
	public bool haveFoundEnemy;
	public Animator m_Animator;
	public Vector3 enemyPos;
	// left-top and right-bottom
	public Vector3[] rectangle;
	public Vector3[] patrolPoints;

	public MsgManager msgm;


	void Awake(){
		reverse = 0;
		curPoint = 0;
		nextPoint = 1;
		speed =4;
		domainWidth = 6.0f;
		pointNum = Random.Range (3, 6);
		findEnemy = false;
		haveFoundEnemy = false;
		patrolPoints = new Vector3[pointNum];
		// Debug.Log (pointNum);
		// draw a domain need to be patroled
		DrawRectangle ();
		// set patrol points
		SetPatrolPoints ();
		m_Animator = gameObject.GetComponent<Animator>();

		float yRotation = Angle(transform.position,patrolPoints [nextPoint],new Vector3(transform.position.x,0,1));
		if (patrolPoints [nextPoint].x < transform.position.x && patrolPoints [nextPoint].z < transform.position.z)
			yRotation += 90; 
		if(patrolPoints [nextPoint].x < transform.position.x) yRotation = -yRotation;
		transform.rotation = Quaternion.Euler(0, yRotation, 0);
		msgm = Singleton<MsgManager>.Instance;
	}
	// Update is called once per frame
	void Update () {
		Patrol ();
	}

	void OnCollisionEnter(Collision collision){
		// Destroy(this.gameObject);
		string name = collision.collider.name;
		if (name.Substring(0,6) == "rooste") {
			m_Animator.SetTrigger("fly_attack");
			msgm.SendMsg("game over","PT","FC");
		}
		else if (name.Substring(0,4) == "Wall") {
			// Debug.Log("hit wall");
			CheckHitWall();
		}
		//Debug.Log("partroller: Name is " + name);
	}
	void OnTriggerEnter(Collider other){
		// Destroy(this.gameObject);
		if(other.name.Substring(0,6) == "rooste")
			FindEnemy ();
		//Debug.Log(other.gameObject.name + ":" + Time.time);
	}
	void OnTriggerStay(Collider other)
	{
		if(other.name.Substring(0,6) == "rooste")
			enemyPos = other.gameObject.transform.position;
	}
	void OnTriggerExit(Collider other){
		if (other.name.Substring(0,6) == "rooste") {
			LostEnemy ();
		}
			
	}
	
	void DrawRectangle(){
		rectangle = new Vector3[2];
		rectangle [0] = transform.position + new Vector3(0,0.5f,-domainWidth/2);
		rectangle [1] = transform.position + new Vector3(domainWidth,0.5f,domainWidth/2);
	}
	
	void SetPatrolPoints(){
		patrolPoints [0] = transform.position;
		patrolPoints[1] = patrolPoints [0] + new Vector3(Random.Range(0.0f,domainWidth),0.5f,-domainWidth/2);
		patrolPoints[2] = patrolPoints [0] + new Vector3(domainWidth,0.5f,Random.Range(-domainWidth/2,domainWidth/2));
		if (pointNum >= 4) {
			patrolPoints[3] = patrolPoints [0] + new Vector3(Random.Range(0.0f,domainWidth),0.5f,domainWidth/2);
		} 
		if (pointNum == 5){
			patrolPoints[4] = patrolPoints [0] + new Vector3(0,0.5f,domainWidth/2);
		}
	}

	public void SetSpeed(int curSpeed){
		speed = curSpeed;
	}
	
	void Patrol(){
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

		if (this.transform.position == patrolPoints [nextPoint]) {

			if(reverse == 0){
				if(nextPoint == pointNum-1){
					curPoint = nextPoint;
					nextPoint = 0;
				}else{
					curPoint = nextPoint;
					nextPoint ++;
				}
			}
			else{
				if(nextPoint == 0){
					curPoint = nextPoint;
					nextPoint = pointNum -1;
				}
				curPoint = nextPoint;
				nextPoint --;
			}
			float yRotation = Angle(transform.position,patrolPoints [nextPoint],new Vector3(transform.position.x,0.5f,transform.position.z+1));
			if(patrolPoints [nextPoint].x < transform.position.x) yRotation = -yRotation;
			transform.rotation = Quaternion.Euler(0, yRotation, 0);
		}
		float step =  speed * Time.deltaTime;

		// check if find enemy
		if (findEnemy) {
			float yRotation = Angle (transform.position, enemyPos, new Vector3 (transform.position.x, 0.5f, transform.position.z + 1));
			if (enemyPos.x < transform.position.x)
				yRotation = -yRotation;
			transform.rotation = Quaternion.Euler (0, yRotation, 0);
			transform.position = Vector3.MoveTowards (transform.position, enemyPos, step);
		} else {
			float yRotation = Angle(transform.position,patrolPoints [nextPoint],new Vector3(transform.position.x,0.5f,transform.position.z+1));
			if(patrolPoints [nextPoint].x < transform.position.x) yRotation = -yRotation;
			transform.rotation = Quaternion.Euler(0, yRotation, 0);
			transform.position = Vector3.MoveTowards (transform.position, patrolPoints[nextPoint], step);
		}
		// check if hit wall
		//CheckHitWall ();
		// move to next point

	}

	float Angle(Vector3 cen, Vector3 first, Vector3 second) 
	{ 
		float dx1, dx2, dy1, dy2, dx3, dy3; 
		float angle; 
		dx1 = first.x - cen.x; 
		dy1 = first.z - cen.z; 
		dx2 = second.x - cen.x; 
		dy2 = second.z - cen.z; 
		dx3 = second.x - first.x;
		dy3 = second.z - first.z;
		float a = (float)Mathf.Sqrt(dx1 * dx1 + dy1 * dy1);
		float b = (float)Mathf.Sqrt(dx2 * dx2 + dy2 * dy2);
		float c =(float)Mathf.Sqrt(dx3 * dx3 + dy3 * dy3);
		
		if (dx1 == 0 && dy1 < 0) return 180; 
		//angle = (float)Mathf.Acos((dx1 * dx2 + dy1 * dy2) / c); 
		angle = (float)Mathf.Acos((a*a + b*b -c*c) / (2*a*b)); 
		if (angle == null)
			return 180;
		return (angle *180) / 3.14159f ; 
	} 


	void FindEnemy(){
		findEnemy = true;
	}

	void LostEnemy(){
		findEnemy = false;
		if (haveFoundEnemy) 
			return;
		msgm.SendMsg ("add score","PT","FC");
		haveFoundEnemy = true;
	}
	
	void CheckHitWall(){
		reverse = (reverse + 1) % 2;
		int temp = curPoint;
		curPoint = nextPoint;
		nextPoint = temp;
	}
}
