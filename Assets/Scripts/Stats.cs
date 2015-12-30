using UnityEngine;
using System.Collections;

public class Stats {
	//Speeds
	public float walkSpeed = 5.0f;
	public float runSpeed = 25.0f;
	public float attackSpeed = 3.0f; //TODO: Eventually, make this private and get the speed from the anim
	public float walkAccel = 8.0f;
	public float runAccel = 40.0f;	

	//Distances
	public float idleDistance = 50.0f;
	public float attackDistance = 5.0f;
	
	//Time lengths in minutes
	public float timeBetweenAttacksMin = 60.0f;
	public float timeBetweenAttacksMax = 180.0f;

	//Other
	public Transform target;
	public int numberOfAttacksMin = 0;
	public int numberOfAttacksMax = 3;
}
