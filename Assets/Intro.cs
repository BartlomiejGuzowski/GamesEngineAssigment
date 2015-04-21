using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	public Texture Book ;
	public Texture FirePlace;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect (0, 0, 800, 800), FirePlace);
		GUI.DrawTexture (new Rect (Screen.width/2, Screen.height/2, 500, 500), Book);
		GUI.TextArea(new Rect ((Screen.width/2)+19, (Screen.height/2)+15, 200, 200), "It  was year 8421 of kindom Calendar " +
			
		    "When revolution had started, Rebel planets had  create federation and chalange old kindom " +
			"and living god who controlded it" +
			"Long and bloody war was started, it had consume many lifes, This " +
			"is story of the finnaly battle, Foreces of two fraction had crush into each other " +
			"Now lets see the events of that day ");
		float timer = Time.realtimeSinceStartup;
	//	Debug.Log (timer);
		if (timer >= 20.00000F) {
	
			Application.LoadLevel(1);
		}
	}
}
