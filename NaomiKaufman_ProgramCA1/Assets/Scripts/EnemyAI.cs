using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent navAgent;
    Vector3 destination;

	[Header("Checks")]
	public bool isSeen;
    public bool isHeard;
    public bool isNear;

	[Header ("Distances")]
	//public float seeLength = 10;
	public float hearLength = 10;
	public float distLength = 10;

    public Transform targetObject;

    Vector3 worldDeltaPosition;
    Vector2 groundDeltaPosition;
    Vector2 velocity = Vector2.zero;

	[Header("Waypoints")]
	int nextIndex;
    public GameObject[] waypoints;

    public float fieldOfViewAngle = 360.0f;
    private BoxCollider col;

	private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
		startPos = transform.position;

		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		destination = NextWaypoint(Vector3.zero);
		
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject)
        {
			// Check is the Player is Visible
			isPlayerSeen();
			// Check is the Player is Audible
			isPlayerHeard();
            // Check is the Player is Visible
            isPlayerNear();


            // Check for visibility and proximity
            if (isSeen && isNear)
            {
                // If Randomball is visible and is close, then SEEK
                seekFunction();
            }
            else if (isSeen && !isNear)
            {
                // If Randomball is visible and not close, then PATROL
                patrolFunction();
            }
            else if (!isSeen && !isHeard)
            {
				// If Randomball is not visible and not audable, then PATROL
				IdleFunction();
			}
            else if (!isSeen && isHeard)
            {
                // If Randomball is visible and not close, then PATROL
                patrolFunction();
            }
			else if (isHeard && isNear)
            {
				patrolFunction();
            }

        }
        else
        {
			// If Player has been destroyed then Idle
			IdleFunction();
		}

        navAgent.SetDestination(destination);


        worldDeltaPosition = navAgent.nextPosition - transform.position;
        groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
        groundDeltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);

        velocity = (Time.deltaTime > 1e-5f) ? groundDeltaPosition / Time.deltaTime : velocity = Vector2.zero;

    }

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player")
		{
			//FOR WHATEVER HAPPENS WHEN ENEMY TOUCHES

		}
	}
	// Seek out Player
	void seekFunction()
	{
		// Seek out the Player
		destination = targetObject.position;
	}
	// Patrol Array 
	void patrolFunction()
	{
		// If within 2.5, then move onto next waypoint in array
		if (Vector3.Distance(transform.position, destination) < 2.5)
		{
			destination = NextWaypoint(destination);
		}
	}
	// Idle at start
	void IdleFunction()
	{
		destination = startPos;
	}

	// Function that loops through waypoints for the Patrol fucntionality
	public Vector3 NextWaypoint(Vector3 currentPosition)
	{
		//Debug.Log(currentPosition);
		if (currentPosition != startPos)
		{
			// Find array index of given waypoint
			for (int i = 0; i < waypoints.Length; i++)
			{
				
				// Once found calculate next one
				if (currentPosition == waypoints[i].transform.position)
				{
					// Modulus operator helps to avoid to go out of bounds
					// And resets to 0 the index count once we reach the end of the array
					nextIndex = Random.Range(0, waypoints.Length);
				}
				Debug.Log(nextIndex);
			}
		}
		else
		{
			// Default is first index in array 
			nextIndex = 0;
		}
		return waypoints[nextIndex].transform.position;
	}

	// Checks if Player is visible using Raycast
	public void isPlayerSeen()
	{

		// Create a vector from the enemy to the player and store the angle between it and forward.
		Vector3 direction = targetObject.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		// Create NavMesh hit var
		NavMeshHit hit;

		// If the Ray cast hits something other than the target, then true is returned, if not false
		// So !hit is used to specify visibility and...
		// If the angle between forward and where the player is, is less than half the angle of view...
		if (!navAgent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
		{
			isSeen = true;
		}
		else
		{
			isSeen = false;
		}
	}

	// Checks if player is Audible using simple distance calculation
	public void isPlayerHeard()
	{
		// If direct distance < 20, then audible
		if (Vector3.Distance(transform.position, targetObject.position) < hearLength)
		{
			isHeard = true;
		}
		else
		{
			isHeard = false;
		}
	}

	// Check is Player is CloseBy based on the NavMesh
	// Distance to Player via the NavMesh is calculated here
	public void isPlayerNear()
	{

		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if (navAgent.enabled)
			navAgent.CalculatePath(targetObject.position, path);

		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;

		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetObject.position;

		// The points inbetween are the corners of the path.
		for (int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}

		// Create a float to store the path length that is by default 0.
		float pathLength = 0;

		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for (int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}

		if (pathLength < distLength)
		{
			isNear = true;
		}
		else
		{
			isNear = false;
		}
	}

}
