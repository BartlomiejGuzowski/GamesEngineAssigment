using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
public	Camera camer;
public	Camera camer1;
public	Camera camer2;
public	Camera camer3;
public	Camera camer4;
public	Camera camer5;
public	Camera camer6;
public	Camera camer7;
public Camera camera8;
	float timer;
	// Use this for initialization
	void Start () {
		camer.enabled = true;
		camer1.enabled = false;
		camer2.enabled = false;
		camer3.enabled = false;
		camer4.enabled = false;
		camer5.enabled = false;
		camer6.enabled = false;
		camer7.enabled = false;
		camera8.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		timer = Time.realtimeSinceStartup;

		if (timer >= 45f) {
			camer.enabled= false;
			camer1.enabled= true;
		}
		if (timer >= 65f) {
			camer1.enabled= false;
			camer2.enabled= true;
		}
		if (timer >= 75f) {
			camer2.enabled= false;
			camer3.enabled= true;
		}
		if (timer > 85f) {
			camer3.enabled =false;
			camer4.enabled= true;
		}
		if (timer >= 95f) {
			camer4.enabled = false;
			camer5.enabled = true;
		}
		if (timer > 100f) {
			camer5.enabled = false;
			camer6.enabled= true;
		}
		if (timer > 110f) {
			camer6.enabled = false;
			camer7.enabled= true;
		}
		if (timer > 120f) {
			camer7.enabled = false;
			camera8.enabled= true;
		}

	
	}
}
