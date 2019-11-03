using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		// Destroy(this.gameObject);
		//Debug.Log(collider.gameObject.name + ":" + Time.time);
	}
}
