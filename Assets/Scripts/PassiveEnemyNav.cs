using UnityEngine;
using System.Collections;

public class PassiveEnemyNav : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;
	bool isTimerRunning;
	float timerTime;
	float chaseTime;
	GameObject parent;

	// Use this for initialization
	void Start () {
		target = null;
		timerTime = 0.0f;
		isTimerRunning = false;
		chaseTime = 5.0f;
		parent = transform.parent.gameObject;
		agent = parent.GetComponentInChildren<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
			agent.SetDestination (target.position);

		if(isTimerRunning == true) {
			if (timerTime >= 0.0f) {
				timerTime--;
				Debug.Log("Timer time: " + timerTime.ToString());
			} else {
				//TODO: Check to see if player is within a certain radius?
				ResetEnemy();
			}
		}
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
