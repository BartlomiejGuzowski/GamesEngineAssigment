using UnityEngine;
using System.Collections;

public class attack: MonoBehaviour {
	public  GameObject Enemy;
	//public GameObject Enemy;
	public Vector3 ToTarget = new Vector3();
	public GameObject target;
	GameObject[] hold;
	GameObject[] hold1;
	float maxSpeed;
	float dist;
	public Vector3 velocity;
	public Vector3 acceleration;
	public Vector3 force;
	public float mass;
	Boid boid ;
	public static  bool isAttacking;
	//public  bool isAttacking;

	public float range = 10; 
	// Use this for initialization
	void Start () {
		range = 200f;
		hold = GameObject.FindGameObjectsWithTag ("Kingdom");
		hold1 = GameObject.FindGameObjectsWithTag ("Federation");
		if (transform.tag == "Federation") {
			Enemy = hold[Random.Range (0,hold.Length)];
		}
		if (transform.tag == "Kingdom") {
			Enemy = hold1[Random.Range(0,hold1.Length)];
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
		if (dist <= range && dist >= 10 && Enemy != null) {
			//	force+= pursue(Enemy); 
			isAttacking = true;
			force += pursue (Enemy);
			acceleration = force / mass;
			velocity += acceleration * Time.deltaTime;
			Vector3.ClampMagnitude (velocity, maxSpeed);

			
			if (float.IsNaN (transform.position.x)) {
				transform.position += velocity * Time.deltaTime;
			}
			if (velocity.magnitude > float.Epsilon) {
				transform.forward = velocity.normalized;
				velocity *= 0.99f;
			}
			
			force = Vector3.zero;
	
			Debug.DrawLine (transform.position, Enemy.transform.position, Color.red, 0.10f);
			lifeSystem.hit = true;

			lifeSystem.hit = false;
			//		ToTarget = transform.position - Enemy.transform.position;
			//	dist = ToTarget.magnitude;
		} else 
		{
			isAttacking = false;
		}
	//	if (dist >= 10) {
	//	}

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
	Vector3 Flee(Vector3 targetPos)
	{
		float panicDistance = 100.0f;
		Vector3 desiredVelocity;
		desiredVelocity = transform.position - targetPos;
		if (desiredVelocity.magnitude > panicDistance)
		{
			//return Vector3.zero;
		}
		desiredVelocity.Normalize();
		desiredVelocity *= maxSpeed;
		return (desiredVelocity - velocity);
	}


}
