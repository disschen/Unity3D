using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable: MonoBehaviour {
	int move_speed = 1;
	
	// change frequently
	int moving_status;	// 0->not moving, 1->moving to middle, 2->moving to dest
	Vector3 dest;
	Vector3 middle;

	void Update() {
		
		if (moving_status == 1) {
			// Debug.Log(transform.position);
			moveTo(transform.position, dest, move_speed * Time.deltaTime);
			// ufoAction.moveTo(this.gameObject,transform.position, dest, move_speed * Time.deltaTime);
			// transform.position = Vector3.MoveTowards (transform.position, dest, move_speed * Time.deltaTime);
			if (transform.position == dest) {
				moving_status = 0;
			}
		}
	}
	

	
	void moveTo(Vector3 pos, Vector3 des, float speed){
		this.transform.position = Vector3.MoveTowards (pos, des, speed);
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