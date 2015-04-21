using UnityEngine;
using System.Collections;

public class teleport : MonoBehaviour {
	public GameObject ship;
	//public GameObject blackHole;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ship.transform.position.z < transform.position.z-10f) {

		//	Debug.Log("PGHFbf");
			Application.LoadLevel(2);
		}
	}
}
