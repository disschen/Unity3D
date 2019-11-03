using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mygame;

public class CrowFactory : MonoBehaviour {
	private List <Crow> used = new List<Crow>(); 
	// Use this for initialization
	public List <Crow> GetCrow(){
		used.Clear ();
		for(int i=0;i<2;i++) {
			for(int j=0;j<3;j++) {
				Crow new_crow = new Crow(i,j);
				used.Add(new_crow);
			}
		}
		Crow fast_crow = new Crow(3,0);
		Crow fast_crow1 = new Crow(3,2);
		Crow fast_crow2 = new Crow(3.5f,1);
		used.Add(fast_crow);
		used.Add(fast_crow1);
		used.Add(fast_crow2);
		return used;
	}
}
