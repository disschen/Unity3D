using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicMoveable : MonoBehaviour
{
	public float r_speed;
	public float u_speed;
	void start(){
		r_speed = 0.0f;
		u_speed = 0.0f;
	}
	
	void setSpeed(float R_speed,float U_speed){
		this.r_speed = R_speed;
		this.u_speed = U_speed;
	}
	
	public void moveTo(Vector3 pos, Vector3 des, float speed){
		float disX = des.x -pos.x;
		float disY = Mathf.Abs( pos.y -des.y);
		float disF =  disX * disX + disY * disY;
		float dis = Mathf.Sqrt(disF);
		float speedX = speed * (disX / dis / 2);
		float speedY = speed * (disY / dis / 2);
		setSpeed(speedX,speedY);
	}
	
	void Start(){
		if(!this.gameObject.GetComponent<Rigidbody>()){
            this.gameObject.AddComponent<Rigidbody>();
        }
	}

	void FixedUpdate(){
		Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
		if(rigid){
			rigid.useGravity = false;
			rigid.AddForce(Vector3.right * r_speed);
			rigid.AddForce(Vector3.up * u_speed);
		}else{
			Debug.Log("no rigid");
		}
	}
}
