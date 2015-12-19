using UnityEngine;
using System.Collections;

public class PassiveEnemyNav : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;
	public float fieldOfView;	//TODO: Use the "perception" stat instead, don't divide by 2
	public float rangeOfView;
	bool isTimerRunning;
	float timerTime;
	float chaseTime;
	GameObject parent;

	// Use this for initialization
	void Start () {
		target = null;
		fieldOfView = 90.0f;
		rangeOfView = 1800.0f;
		timerTime = 0.0f;
		isTimerRunning = false;
		chaseTime = 5.0f;
		parent = transform.parent.gameObject;
		agent = parent.GetComponentInChildren<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			//TODO: Make this a little rougher. Use transform.position?
			if (agent.transform == target.transform) {
				//TODO: New random target in area. Start timer.
			}

			if(target.tag == "Player") {
				agent = parent.GetComponentInChildren<NavMeshAgent> ();
				//Vector3 enemyHeading = agent.transform.forward.normalized;
				//Vector3 headingToPlayer = (target.transform.position - agent.transform.position).normalized;
				//float dotProduct = Vector3.Dot(enemyHeading, headingToPlayer);

				if(CanISeeTarget() == true) {
					agent.SetDestination (target.position);
				}
			} else {
				agent.SetDestination(target.position);
			}
		}

		if(isTimerRunning == true) {
			if (timerTime >= 0.0f) {
				//TODO: Better subtraction, try to get once per second
				timerTime--;
				Debug.Log("Timer time: " + timerTime.ToString());
			} else {
				//TODO: Check to see if player is within a certain radius?
				ResetEnemy();
			}
		}
	}

	bool CanISeeTarget() {
		RaycastHit hit;
		Vector3 rayDir = target.transform.position - agent.transform.position;

		//If the target is close to the object, and in front, return true
		float angle = Vector3.Angle (rayDir, agent.transform.forward);
		float distance = Vector3.Distance (agent.transform.position, target.transform.position);
		if ( angle < fieldOfView / 2.0f && distance < rangeOfView) {
			Debug.Log("In front of enemy");
			return true;
		}

		//Is the player within the enemies field of view
		if ((Vector3.Angle (rayDir, agent.transform.forward)) < fieldOfView / 2.0f) {
			Debug.Log ("Player within FoV");

			if (Physics.Raycast (agent.transform.position, rayDir, out hit, rangeOfView)) {
				if (hit.collider.gameObject == target) {
					Debug.Log ("Raycast hit player");
					return true;
				} else {
					Debug.Log ("Raycast did not hit player");
					return false;
				}
			}
		} else {
			return false;
		}

		return false;
	}
	//Player enters Chase Radius
	void OnTriggerEnter (Collider aTriggerer) 
	{
		Debug.Log ("trigger collided");
		if (aTriggerer.gameObject.tag == "Player") {
			SetTarget(aTriggerer.transform);
			Debug.Log ("trigger collided with player");
		}
	}

	void OnTriggerExit(Collider aTriggerer) 
	{
		Debug.Log ("trigger left");
		if (aTriggerer.gameObject.tag == "Player") {
			StartTimer(chaseTime);
			Debug.Log ("trigger left by player");
		}
	}

	void SetTarget(Transform aTarget)
	{
		Debug.Log ("Set target");
		target = aTarget;
	}

	void StartTimer(float aTimerLength)
	{
		Debug.Log ("Set timer");
		timerTime = aTimerLength;
		isTimerRunning = true;
	}

	void StopTimer()
	{
		Debug.Log ("Stop timer");
		timerTime = 0.0f;
		isTimerRunning = false;
	}

	void ResetEnemy()
	{
		SetTarget(transform);
		StopTimer();
	}
}
