using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

    public Path path = new Path ();

    public Vector3 seekTarget;
//	public Vector3  AriveDest;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
	public Vector3 OffSetSet ;
	public Vector3 AriveTarget ; 
	public Vector3[] waypoint ; 

	public GameObject PathFolowObject;
	public float mass;
    public float maxSpeed;
	public float slowdistance;
    public GameObject pursueTarget;
	public GameObject Leader ;
	public bool AriveEnable;
	public bool OffsetEnable;
    public bool seekEnabled;
    public bool pursueEnabled;
	public bool FolowEneable; 
	public bool LoopPath;
	GameObject[] Child;
	public Boid()
    {
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 10.0f;

    }

	// Use this for initialization
	void Start () {

		if (PathFolowObject != null) {
						for (int i = 0; i < PathFolowObject.transform.childCount; i ++) {
								
							path.waypoints.Add (PathFolowObject.transform.GetChild (i).position);
							 			
			}
				}


		path.looped = LoopPath;

		//waypoint [5] = new Vector3 (10, 0, 20);
		if (OffsetEnable) {
			OffSetSet = Leader.transform.position - transform.position;		
		}
	}
	Vector3 FollowPath(){
				Vector3 Next = path.NextWaypint ();
				float dist = (transform.position - Next).magnitude;
				float waypointDistance = 5;
				
				if (dist < waypointDistance ){
						path.Advance ();		
				}
				if (path.looped && path.islast ()) {
						return Arive (Next);
				} else {
					return	Seek (Next);
				}
		//return Vector3.zero;
		}
	Vector3 Offset(Vector3 offset){
		Vector3 target = Vector3.zero;
		target = Leader.transform.TransformPoint (offset);
		float dist = (target - transform.position).magnitude;
		float time = dist / maxSpeed;
		target = target + ( time * Leader.GetComponent<Boid>().velocity);
		return (target);
	
	}

	Vector3 OffsetPursude(GameObject LeaderToFollow){

		Vector3 targetpos =LeaderToFollow.transform.TransformPoint(OffSetSet);
		Vector3 totarget = targetpos - transform.position;
		float distance = totarget.magnitude;
		float tiime = distance / maxSpeed;
		Vector3 target1 = LeaderToFollow.GetComponent<Boid> ().velocity * tiime + LeaderToFollow.transform.position;
	//	return (target1);
		return Seek(target1);
	}


	Vector3 Arive(Vector3 AriveAtTarget){
	//	AriveAtTarget = ToTarget;
		Vector3 EndLine;
		 
		//Debug.DrawLine (AriveAtTarget,E );
		Vector3 AriveTarget = AriveAtTarget-transform.position;

		float Distance = AriveTarget.magnitude; 
		float rampage = Distance / slowdistance * maxSpeed; 
		float Clamped = Mathf.Min (rampage, maxSpeed);
		Vector3 desired = Clamped * (AriveTarget / Distance);
		return (desired - velocity);


	
	}


    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 desired = seekTarget - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    Vector3 pursue(GameObject pursueTarget)
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(seekTarget, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        if (pursueEnabled)
        {
            force += pursue(pursueTarget);
        }
        if (seekEnabled)
        {
            force += Seek(seekTarget);
        }        
        if (AriveEnable) {
			force += Arive(AriveTarget);		
		}
		if (OffsetEnable) {
			force +=  OffsetPursude(Leader); //Offset(OffSetSet);		
		}
		if (FolowEneable) {
			force += FollowPath();		
		}

		acceleration =  force / mass;
        velocity += acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, maxSpeed);
        
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity.normalized;
        }

        force = Vector3.zero;
	}
}
