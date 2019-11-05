using UnityEngine;
using System.Collections;

public class ParticleSea : MonoBehaviour {
	
	public ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particlesArray;
	
	public int bigResolution = 120;
	public int smallResolution = 12;
	public float radius = 5f;
	public float smallRadius = 0.5f;

	private Vector3 Center;
	private Vector3[] edgesArray;

	private float rotateSpeed = 10;

	void Start() {
		particlesArray = new ParticleSystem.Particle[bigResolution * smallResolution + 100];
		particleSystem.maxParticles = bigResolution * smallResolution;
		particleSystem.Emit(bigResolution * smallResolution);
		particleSystem.GetParticles(particlesArray);

		Center = Vector3.zero;
		edgesArray = new Vector3[bigResolution+1];
		for (int i =0; i < bigResolution; i ++) {
			float cornerAngle = 2f * Mathf.PI / (float) bigResolution * i;
			float xPos = Mathf.Cos(cornerAngle) * radius;
			float zPos = Mathf.Sin(cornerAngle) * radius;
			edgesArray[i] = Center + new Vector3(xPos,0,zPos);
			// Debug.Log (edgesArray[i]);
		}

		for(int i = 0; i < bigResolution; i++) {
			for(int j = 0; j <smallResolution; j++) {
				float div = radius / smallRadius;
				float xx = edgesArray[i].x/div;
				float zz = edgesArray[i].z/div;
				float cornerAngle = 2f * Mathf.PI / (float) smallResolution * j;
				particlesArray[i * smallResolution + j].position = new Vector3(xx*Mathf.Cos (cornerAngle), smallRadius * Mathf.Sin(cornerAngle), zz*Mathf.Cos (cornerAngle)) + edgesArray[i];
			}
		}

		particleSystem.SetParticles(particlesArray, particlesArray.Length);
		Debug.Log (rotateSpeed * Time.deltaTime);
	}

	void Update() {
		this.transform.RotateAround (Center, Vector3.back, 20 * Time.deltaTime);
	}
}
