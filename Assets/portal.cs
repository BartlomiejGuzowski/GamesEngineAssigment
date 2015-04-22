using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour {
	float timer;
	public
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		timer= Time.realtimeSinceStartup;
		if(timer >= 49f) {
			GameObject.Destroy(transform.gameObject);
	}
}
}
