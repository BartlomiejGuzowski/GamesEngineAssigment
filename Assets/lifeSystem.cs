using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class lifeSystem : MonoBehaviour {
	public float Life; 
	public static bool hit;
	public GameObject Enemy; 
	public float range;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Life == 0) {
			GameObject.DestroyObject(transform.gameObject);
		}
		attack (Enemy);
	}
	void attack(GameObject enemypos){
		Vector3 target = enemypos.gameObject.transform.position - transform.position;
		float dist = target.magnitude;
		if (dist < range) {
			Debug.DrawLine(transform.position,enemypos.transform.position,Color.red,2);
			Debug.DrawRay(transform.position,enemypos.transform.position,Color.green,200.0f);
			//LineDrawer.DrawLine(transform.position, transform.position + transform.forward * 10.0f, Color.red);

		}
	
	}

}
