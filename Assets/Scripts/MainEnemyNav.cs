using UnityEngine;
using System.Collections;

public class MainEnemyNav : MonoBehaviour {
	//TODO: If the player see this, increase speed
	public Transform playerTarget;
	Vector3 fleeTarget;

	//Time lengths in minutes
	public float timeBetweenAttacksMin = 60.0f;
	public float timeBetweenAttacksMax = 180.0f;

	//Speeds
	public float stalkSpeed = 5.0f;
	public float chargeSpeed = 25.0f;
	public float attackSpeed = 3.0f;
	public float stalkAccel = 8.0f;
	public float chargeAccel = 40.0f;

	//Distances
	public float idleDistance = 50.0f;
	public float attackDistance = 5.0f;

	//Other
	public int numberOfAttacksMin = 0;
	public int numberOfAttacksMax = 3;
	int attacksDoneThisRun = 0;
	bool attackingThePlayer;
	bool fleeingThePlayer;

	Timer attackTimer;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		Debug.Log ("Main Enemy Start");

		attackTimer = new Timer ();
		ClearTimer ();
		SetStalkTimer ();

		fleeTarget = Vector3.zero;
		fleeingThePlayer = false;

		agent = GetComponent<NavMeshAgent> ();
		agent.stoppingDistance = idleDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer.GetID () != "NULLID")
			attackTimer.TimerUpdate ();

		if (playerTarget != null) {
			float distance = float.MaxValue;

			if(fleeingThePlayer == false) {
				agent.SetDestination (playerTarget.position);
				
				distance = Vector3.Distance(transform.position, playerTarget.position);
			} else {
				distance = Vector3.Distance(transform.position, fleeTarget);
			}

			//Debug.Log("Distance: " + distance.ToString());

			if(distance <= agent.stoppingDistance && distance != float.MaxValue) {
				transform.RotateAround(playerTarget.transform.position, Vector3.up, agent.speed * Time.deltaTime);

				if(fleeingThePlayer == true) {
					BeginChasingPlayer();
				}
				else if(agent.stoppingDistance == attackDistance && attackTimer.GetID() == "NULLID") {
					Debug.Log("Attack Distance");
					SetAttackTimer ();
				}
				else if (agent.stoppingDistance == idleDistance && attackTimer.GetID() == "NULLID") {
					Debug.Log("Idle Distance");
					SetStalkTimer ();
				}
			}
		}
	}

	//ATTACKING STUFF
	void BeginAttackRun() {
		ClearTimer ();

		Debug.Log("Beginning attack run");

		agent.stoppingDistance = attackDistance;
		agent.speed = chargeSpeed;
		agent.acceleration = chargeAccel;

		//TODO: Choose a number of attacks to do
		//TODO: Keep track of those attacks, and when they're hit call EndAttackRun
		//TODO: Play a noise
	}

	void EndAttackRun() {
		ClearTimer ();

		Debug.Log ("Ending attack run");

		attacksDoneThisRun = 0;

		FleeFromPlayer ();
	}

	//Nav Stuff
	void FleeFromPlayer () {
		Vector3 ranDir = Random.insideUnitCircle * idleDistance * 2.0f;

		ranDir.z = ranDir.y;
		ranDir.y = 0.0f;
		ranDir += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(ranDir, out hit, idleDistance * 2.0f, 1);
		fleeTarget = hit.position;
		
		agent.SetDestination (fleeTarget);

		fleeingThePlayer = true;
	}

	void BeginChasingPlayer() {
		agent.stoppingDistance = idleDistance;
		agent.speed = stalkSpeed;
		agent.acceleration = stalkAccel;
		fleeingThePlayer = false;
		fleeTarget = Vector3.zero;
	}

	//TIMER STUFF
	void SetAttackTimer () {
		int attackNumber = Random.Range (numberOfAttacksMin, numberOfAttacksMax);
		float waitTime = attackSpeed * (attackNumber * 1.0f);

		attackTimer.SetParams ("MainEnemyAttackTimer", waitTime, EndAttackRun);
	}

	void SetStalkTimer() {		
		float waitTime = Random.Range (timeBetweenAttacksMin, timeBetweenAttacksMax);

		attackTimer.SetParams ("MainEnemyStalkTimer", 15.0f, BeginAttackRun);	//TODO: Set the 15.0f to waitTime
	}

	void ClearTimer() {
		attackTimer.SetID ("NULLID");	//TODO: Make variable
	}
}
