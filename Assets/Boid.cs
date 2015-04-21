using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boid : MonoBehaviour {



	[Header("Seek")]
	public Vector3 seekTarget;
	public bool seekEnabled;
	
	[Header("Arrive")]    
	public Vector3 arriveTarget;
	
	public Vector3 velocity;
	public Vector3 acceleration;
	public Vector3 force;
	public float mass;
	public float maxSpeed;
	public GameObject pursueTarget;
	
	public Path path;
	public List<GameObject> Ships = new List<GameObject>();
	public List<Vector3> OFFSET = new List<Vector3> ();
	public bool pursueEnabled;
	public bool arriveEnabled;
	
	public bool offsetPursueEnabled;
	public GameObject leader;
	public Vector3 offset;
	public Vector3 seekTargetPos;
	[Header("Path Following")]
	public  bool pathFollowingEnabled;
	public  bool Looped;
	public  GameObject PathFolowObject;
	
	public Boid()
	{
		mass = 1;
		velocity = Vector3.zero;
		force = Vector3.zero;
		acceleration = Vector3.zero;
		maxSpeed = 10.0f;
		
		path = new Path();
		Looped = false;
		
	}
	
	// Use this for initialization
	void Start () {
	
		if (PathFolowObject != null) {

		for (int i = 0; i < PathFolowObject.transform.childCount; i ++) {
			path.waypoints.Add (PathFolowObject.transform.GetChild (i).position);
	//			Debug.Log(path.waypoints[i]);
			}
		}
		if (offsetPursueEnabled)
		{
			if (leader != null)
			{
				offset = leader.transform.position - transform.position;
			}
		}
		/*
		path.looped = Looped;
		if (offsetPursueEnabled)
		{
			for(int i = 0;i <= Ships.Count;i++)
			{
				Debug.Log("Old Value"+Ships[i].transform.position+" Of "+i);
				OFFSET[i] = Ships[i].transform.position - transform.position;
				Debug.Log(OFFSET[i]);
				//	offset = Ships[i].transform.position - transform.position;

				Debug.Log("New Value"+Ships[i].transform.position+" Of "+i);
			}
			//	Debug.Log(Ships[i].transform.position);
			
				if (leader != null)
				{
			//	int i=0;
			/*	foreach( GameObject value in Ships)
					{

						Ships[i].transform.position = Ships[i].transform.position - transform.position;
						Debug.Log("New pos:"+Ships[i].transform.position+"\n");
					i++
				}*/
		    //	}
	//		Ships.
		//		Debug.Log(offsetPursueTarget.transform.name);
		//		Debug.Log(offset);

		
	//	}
		path.looped = Looped;
	}

public  Vector3 FollowPath()
	{
		Vector3 next = path.NextWaypint();
		float dist = (transform.position - next).magnitude;
		float waypointDistance = 5;
		if (dist < waypointDistance)
		{
			next = path.Advance();
		}
		if (! path.looped && path.islast())
		{
			return Arrive(next);
		}
		else
		{
			return Seek(next);
		}
	}
	/*
	Vector3 Formation(GameObject FormationMembers,Vector3 Pos){
		Vector3 FormationPos = FormationMembers.transform.root.TransformPoint (Pos);
		Vector3 Leader = FormationPos - transform.position;
		float distance = Leader.magnitude;
		float time = distance / maxSpeed;
		Vector3 target = FormationPos + FormationMembers.GetComponent<Boid>().velocity * time;
		return target;
	
	}
*/

	Vector3 OffsetPursuit(Vector3 offset)
	{
		Vector3 target = Vector3.zero;
		target = leader.transform.TransformPoint(offset);
		
		float dist = (target - transform.position).magnitude;
		
		float lookAhead = (dist / maxSpeed);
		
		target = target + (lookAhead * leader.GetComponent<Boid>().velocity);
		
	//	Utilities.checkNaN(target);
		return Arrive(target);
	}
	
	

	
public Vector3 Seek(Vector3 seekTarget)
	{
		Vector3 desired = seekTarget - transform.position;
		desired.Normalize();
		desired *= maxSpeed;

	//	LineDrawer.DrawTarget(seekTarget, Color.blue);
		return desired - velocity;
	}
	
public 	Vector3 Arrive(Vector3 arriveTarget)
	{
		Vector3 toTarget = arriveTarget - transform.position;

		float distance = toTarget.magnitude;
		
		float slowingDistance = 0.5f;
		
	//	LineDrawer.DrawSphere(arriveTarget, slowingDistance, 10, Color.yellow);
		
		float ramped = (distance / slowingDistance) * maxSpeed;
		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = (toTarget / distance) * clamped;
		return desired - velocity;
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
	
	// Update is called once per frame
	void Update () {
		
	//	if (attack.target!=null)
	//	{
			//Debug.Break();
	//		force += pursue(attack.target);
	//	}
		if (attack.isAttacking) {
		//if (gameObject.GetComponent<attack>().isAttacking) {
		//	force += pursue(attack.Enemy);
		//	Debug.Break();
		//	//force += pursue(gameObject.GetComponent<attack>().Enemy);
		}
		if (seekEnabled)
		{
			force += Seek(seekTarget);
		}        
		if (arriveEnabled)
		{
			force += Arrive(arriveTarget);
		}
		
		if (offsetPursueEnabled)// && transform != null)
		{
			force+=OffsetPursuit(offset);

		}
		if (pathFollowingEnabled && attack.isAttacking!= true)
		{
	//		path.Draw();
			force += FollowPath();
		}
		acceleration =  force / mass;
		velocity += acceleration * Time.deltaTime;
		Vector3.ClampMagnitude(velocity, maxSpeed);
		

		
		transform.position += velocity * Time.deltaTime;
		
		if (velocity.magnitude > float.Epsilon)
		{
			transform.forward = velocity.normalized;
		//	velocity *= 0.99f;
		}
		
		force = Vector3.zero;
	}
}
