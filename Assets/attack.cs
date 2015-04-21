using UnityEngine;
using System.Collections;

public class attack: MonoBehaviour {
	public  GameObject Enemy;
	//public GameObject Enemy;
	public Vector3 ToTarget = new Vector3();
	public GameObject target;
	float maxSpeed;
	float dist;
	public Vector3 velocity;
	public Vector3 acceleration;
	public Vector3 force;
	public float mass;
	Boid boid ;
	public static  bool isAttacking;
	//public  bool isAttacking;

	public float range; 
	// Use this for initialization
	void Start () {
		if (transform.tag == "Federation") {
			Enemy = GameObject.FindGameObjectWithTag ("Kingdom");
		}
		if (transform.tag == "Kingdom") {
			Enemy = GameObject.FindGameObjectWithTag ("Federation");
		}
	//	Enemy = target;
	//	Debug.Log (Enemy);
		//gameObject.tag = "Player";
		//boid = new Boid ();
		if (Enemy != null) {
			ToTarget = transform.position - Enemy.transform.position;
			dist = ToTarget.magnitude;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (dist <= range && Enemy != null) {
			isAttacking = true;
			force += pursue(Enemy);
			acceleration =  force / mass;
		//	velocity += acceleration * Time.deltaTime;
			Vector3.ClampMagnitude(velocity, maxSpeed);
			
			
			
			transform.position += velocity * Time.deltaTime;
			
			if (velocity.magnitude > float.Epsilon)
			{
				transform.forward = velocity.normalized;
				velocity *= 0.99f;
			}
			
			force = Vector3.zero;
			//transform.gameObject.GetComponents<Boid>()
	//		Debug.Log("_________________________________________________");
		//	transform.GetComponent<Boid>().pursue(Enemy);
		//	Debug.Log("_________________________________________________");
			///	boid.pursue(Enemy);
			/*Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			
			
			
			if(Physics.Raycast(ray, out hit, 100))
			{
				
				if(hit.rigidbody)
				{
					Debug.Break();
					hit.rigidbody.AddForceAtPosition(transform.forward  
					                                 * 10, hit.point);
				}
			}*/
			Debug.DrawLine (transform.position, Enemy.transform.position, Color.red, 0.10f);
			lifeSystem.hit= true;

			lifeSystem.hit=false;
	//		ToTarget = transform.position - Enemy.transform.position;
		//	dist = ToTarget.magnitude;
		}
		ToTarget = transform.position - Enemy.transform.position;
		dist = ToTarget.magnitude;
	//	ToTarget = transform.position - Enemy.transform.position;
		//dist = ToTarget.magnitude;
	}
	public	Vector3 pursue(GameObject pursueTarget)
	{
		Vector3 toTarget = pursueTarget.transform.position - transform.position;
		float distance = toTarget.magnitude;
		
		float time = distance / maxSpeed;
		Vector3 target =
			pursueTarget.transform.position +
				pursueTarget.GetComponent<Boid>().velocity * time;
		Debug.DrawLine(target, target + Vector3.forward);
		return Seek(target);
	}
	public Vector3 Seek(Vector3 seekTarget)
	{
		Vector3 desired = seekTarget - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		
		//	LineDrawer.DrawTarget(seekTarget, Color.blue);
		return desired - velocity;
	}

}
