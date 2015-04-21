using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Formation : MonoBehaviour {
	public List<GameObject> Ships = new List<GameObject>();
	public List<Vector3> OFFSET = new List<Vector3> ();
	//public List<GameObject> AttackShips = new List<GameObject> ();
	//public GameObject[] AttackShips;
	public GameObject PathFolowObject;
	public bool ChaseEnemy;
	GameObject enemy ;
	public Vector3 FormationForce;
	public Vector3 FormationMVelocity;
	public Vector3 FomationAcceleration;
	public float FomationMass;
	public float FormationMaxSpped ;
	public Vector3 pos = new Vector3(0,0,0);
	public int i = 0;
	Boid boid ;
	public Path path;
	int m =0;
	public bool pathFollowingEnabled;
	public bool Looped;
	public float timer ;
	/// <summary>
	/// Leader data
	/// </summary>
	
	public Vector3 leadervelocity;
	public Vector3 leaderacceleration;
	public Vector3 leaderforce;
	public float leadermass;
	public float leadermaxSpeed;



	public int attShipNo;


	public Formation(){
		FomationMass = 1;
		FormationMVelocity = Vector3.zero;
		FormationForce = Vector3.zero;
		FomationAcceleration = Vector3.zero;
		FormationMaxSpped = 10.0f;

		leadermass = 1;
		leadervelocity = Vector3.zero;
		leaderforce = Vector3.zero;
		leaderacceleration = Vector3.zero;
		leadermaxSpeed = 10.0f;
		
		path = new Path();
		Looped = false;
	}
	//
	// Use this for initialization
	void Start () {

	//	AttackShips = GameObject.FindGameObjectsWithTag ("Player");

//		attShipNo = AttackShips.Length;

	//	for (int i=0; i<AttackShips.Count; i++) {
	//		AttackShips.Add(GameObject.FindGameObjectsWithTag ("Player") as GameObject);
	//	}
		if (PathFolowObject != null) {
			
			for (int i = 0; i < PathFolowObject.transform.childCount; i ++) 
			{

				path.waypoints.Add (PathFolowObject.transform.GetChild (i).position);
			
			}
		}

	for (int i=0; i<OFFSET.Count; i++) {
		OFFSET[i] = Ships[i].transform.position - transform.position;
	}
	//for (int i=0; i<OFFSET.Count; i++) {
	//		Ships[i] = Ships[i];
	//}
	
		//	dist = (transform.position + OFFSET [0] - Ships [0].transform.position).magnitude;
		//Debug.Log (dist);
	//	Leader = transform.gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		float delay = Time.realtimeSinceStartup;
		//Debug.Log(delay);
		//Debug.Log (delay);
		if (delay >= timer) {
			if (Ships != null) {

				FormationUpdate ();
			
			}
			LeaderUpdate ();
		}
		//if (Ships != null) {
	//		FormationUpdate ();
	//	}
	}
	void LeaderUpdate(){
		if (PathFolowObject != null) {
			if (pathFollowingEnabled) {
				//		path.Draw();
				leaderforce += FollowPath ();
			}
			
			
			leaderacceleration = leaderforce / leadermass;
			leadervelocity += leaderacceleration * Time.deltaTime;
			Vector3.ClampMagnitude (leadervelocity, leadermaxSpeed);
			

			transform.position += leadervelocity * Time.deltaTime;
			
			if (leadervelocity.magnitude > float.Epsilon) {
				transform.forward = leadervelocity.normalized;
				leadervelocity *= 0.99f;
			}
			
			leaderforce = Vector3.zero;
		}
	}
	void FormationUpdate(){
		if (transform != null) {
			//			Debug.Log(boid.Seek(pos));
		//	foreach (GameObject member in Ships)
				for(int k =0 ;k< Ships.Count;k++)
			{
			
			//    if({
				//	if(ChaseEnemy){
					
			//	Ships[k].GetComponent<Boid>().pursue(AttackShips[k].GetComponent<attack>().Enemy);
				//	}
				
			//	else
			//	{
				//		if (Seek (transform.position, member, OFFSET [i]) != Vector3.zero) {
				FormationForce += Seek (transform.position, Ships[k], OFFSET [i]);
				//FormationForce += Arrive (transform.position, member, OFFSET [i]);
				//FormationForce += OffsetPursuit(OFFSET[i],member);
				FomationAcceleration = FormationForce / FomationMass;
			
				FormationMVelocity += FomationAcceleration * Time.deltaTime;
				Vector3.ClampMagnitude (FormationMVelocity, FormationMaxSpped);
				
				if (FormationMVelocity.magnitude > float.Epsilon) {
					Ships[k].transform.forward = FormationMVelocity.normalized;
					FormationMVelocity *= 0.99f;
				}
				
				if(FormationMVelocity.x != float.NaN && FormationMVelocity.z != float.NaN){ 
				Ships[k].transform.position += FormationMVelocity * Time.deltaTime;
				}
					i++;
				if (i == OFFSET.Count) {
					i = 0;
				}
				FormationForce = Vector3.zero;
			//	}
			}
		} 
		else {
			
			//	transform = Ships [m];
			m++;
		}
	}
/*		FomationAcceleration = FormationForce / FomationMass;
		FormationMVelocity += FomationAcceleration * Time.deltaTime;
		Vector3.ClampMagnitude (FormationMVelocity, FormationMaxSpped);
		
		if (FormationMVelocity.magnitude > float.Epsilon) {
	//		transform.forward = FormationMVelocity.normalized;
			FormationMVelocity *= 0.99f;
		}
		
		
	//	transform.position += FormationMVelocity * Time.deltaTime;
		i++;
		if (i == OFFSET.Count) {
			i = 0;
		}
		FormationForce = Vector3.zero;

//		FormationForce+= FollowPath ();
		//FormationForce += Boid.FollowPath ();
		//			FormationForce += boid.Seek(pos);
	//	transform.position += FormationMVelocity * Time.deltaTime;

		
	
		//	
	}
	*/
//	}
//	}

	Vector3 OffsetPursuit(Vector3 offset,GameObject fomation)
	{
		Vector3 target = Vector3.zero;
		target = transform.TransformPoint(offset);
		
		float dist = (target - fomation.transform.position).magnitude;
		
		float lookAhead = (dist / FormationMaxSpped);
		
		target = target + (lookAhead * FormationMVelocity);

	//	Utilities.checkNaN(target);
		return Arrive(target);
	}
	public Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - Ships[0].transform.position;
		
		float slowingDistance = 8.0f;
		float distance = toTarget.magnitude;
		if (distance == 0.0f)
		{
			return Vector3.zero;
		}
		const float DecelerationTweaker = 10.3f;
		float ramped = FormationMaxSpped* (distance / (slowingDistance * DecelerationTweaker));
		
		float clamped = Math.Min(ramped, FormationMaxSpped);
		Vector3 desired = clamped * (toTarget / distance);
		
		return desired - FormationMVelocity;
	}

	Vector3 Seek(Vector3 seekTarget,GameObject ship,Vector3 offset)
	{

			Vector3 desired = (seekTarget + offset) - ship.transform.position;//+ offset;
			desired.Normalize ();
			desired *= FormationMaxSpped;
		
			//	LineDrawer.DrawTarget(seekTarget, Color.blue);
			return desired - FormationMVelocity;
	}
	Vector3 FollowPath()
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
			return ArriveLeader(next);
		}
		else
		{
			return SeekLeader(next);
		}
	}
	public Vector3 SeekLeader(Vector3 seekTarget)
	{
		Vector3 desired = seekTarget - transform.position;
		desired.Normalize();
		desired *= leadermaxSpeed;
		//	LineDrawer.DrawTarget(seekTarget, Color.blue);
		return desired - leadervelocity;
	}


	public 	Vector3 ArriveLeader(Vector3 arriveTarget)
	{
		Vector3 toTarget = arriveTarget - transform.position;
		
		float distance = toTarget.magnitude;
		
		float slowingDistance =10f;
		
		//	LineDrawer.DrawSphere(arriveTarget, slowingDistance, 10, Color.yellow);
		
		float ramped = (distance / slowingDistance) * leadermaxSpeed;
		float clamped = Mathf.Min(ramped, leadermaxSpeed);
		Vector3 desired = (toTarget / distance) * clamped;
		return desired - leadervelocity;
	}

/*	void Seek1(Vector3 seekTarget,GameObject ship)
	{
		Vector3 desired = seekTarget - ship.transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		//	LineDrawer.DrawTarget(seekTarget, Color.blue);
	//	return desired - velocity;
	}*/
}
