using UnityEngine;
using System.Collections;

public class MainEnemyNav : MonoBehaviour {
	//TODO: Have it go near the enemy, to ~50-100 meters away. This should be far enough away that it is out of the player's LoS. Maybe raycast to the player until the cast fails?
	//TODO: Have it circle the enemy once at the radius
	//TODO: Have it attack the enemy on random intervals, with min and max wait time as public vars
	//TODO: Have it perform a random number of attacks (should at most remove 1/3 of player's max health), with min and max number of attacks as public vars
	//TODO: Have it circle the player while it attacks (have a timer run to simulate attack duration) then flee to edge of radius
	//TODO: Maybe hack this out by placing a collider on the player that randomly goes down then back up?
	public Transform target;
	public float chaseTime = 15.0f;
	Timer chaseTimer;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		chaseTimer = new Timer ();	//TODO: See if I need to update this here, or if it updates on its own
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
			agent.SetDestination (target.position);
	}
}
