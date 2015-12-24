using UnityEngine;
using System.Collections;

public class MainEnemyNav : MonoBehaviour {
	//TODO: If the player see this, increase speed
	//TODO: Have it attack the enemy on random intervals, with min and max wait time as public vars
	//TODO: Have it perform a random number of attacks (should at most remove 1/3 of player's max health), with min and max number of attacks as public vars
	//TODO: Have it circle the player while it attacks (have a timer run to simulate attack duration) then flee to edge of radius
	//TODO: Maybe hack this out by placing a collider on the player that randomly goes down then back up?
	public Transform target;

	//Time lengths in minutes
	public float timeBetweenAttacksMin = 60.0f;
	public float timeBetweenAttacksMax = 180.0f;

	//Speeds
	public float stalkSpeed = 5.0f;
	public float chargeSpeed = 20.0f;
	public float attackSpeed = 1.0f; //TODO: Eventually, make this private and get the speed from the anim

	//Distances
	public float idleDistance = 50.0f;
	public float attackDistance = 5.0f;

	//Other
	public int numberOfAttacksMin = 0;
	public int numberOfAAttacksMax = 3;
	int attacksDoneThisRun = 0;
	bool attackingThePlayer;

	Timer attackTimer;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		Debug.Log ("Main Enemy Start");

		ClearTimer ();
		SetStalkTimer ();

		agent = GetComponent<NavMeshAgent> ();
		agent.stoppingDistance = idleDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer != null)	//TODO: Figure out why == and != seem to be switched
			attackTimer.TimerUpdate ();

		if (target != null) {
			agent.SetDestination (target.position);
			
			float distance = Vector3.Distance(transform.position, target.position);
			//Debug.Log("Distance: " + distance.ToString());

			if(distance <= agent.stoppingDistance) {
				transform.RotateAround(target.transform.position, Vector3.up, agent.speed * Time.deltaTime);

				if(agent.stoppingDistance == attackDistance && attackTimer == null) {
					Debug.Log("Attack Distance");
					SetAttackTimer ();
				}
				else if (agent.stoppingDistance == idleDistance && attackTimer == null ) {
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

		//TODO: Choose a number of attacks to do
		//TODO: Keep track of those attacks, and when they're hit call EndAttackRun
		//TODO: Play a noise
	}

	void EndAttackRun() {
		ClearTimer ();

		Debug.Log ("Ending attack run");

		agent.stoppingDistance = idleDistance;
		agent.speed = stalkSpeed;

		attacksDoneThisRun = 0;

		//TODO: Choose a point 200 meters away from the current point and set it as target. When the enemy is within circleDistance, set target to be the player and change enemy speed
	}

	//TIMER STUFF
	void SetAttackTimer () {
		if (attackTimer == null) {
			attackTimer = new Timer ();
		}

		int attackNumber = Random.Range (numberOfAttacksMin, numberOfAAttacksMax);
		float waitTime = attackSpeed * (attackNumber * 1.0f);

		attackTimer.SetParams ("MainEnemyAttackTimer", waitTime, EndAttackRun);
	}

	void SetStalkTimer() {		
		if (attackTimer == null) {
			attackTimer = new Timer ();
		}

		float waitTime = Random.Range (timeBetweenAttacksMin, timeBetweenAttacksMax);

		attackTimer.SetParams ("MainEnemyStalkTimer", 15.0f, BeginAttackRun);
	}

	void ClearTimer() {
		if (attackTimer != null) {
			Destroy (attackTimer);
		}
		attackTimer = null;
	}
}
