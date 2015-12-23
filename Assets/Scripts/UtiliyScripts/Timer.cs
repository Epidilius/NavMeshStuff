using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	float timeLeft;
	string id;
	bool isRunning;
	//TODO: Function pointer
	//TODO: Array of timers?

	// Use this for initialization
	public void Start () {
	
	}

	public void SetParams(float aLength, string aID) {

	}
	
	// Update is called once per frame
	void Update () {
		if(isRunning == true) {
			if (timeLeft >= 0.0f) {
				//TODO: Better subtraction, try to get once per second
				timeLeft -= Time.deltaTime;
				Debug.Log("Timer time: " + timeLeft.ToString());
			} else {
				//TODO: Call delegate of class that created timer
			}
		}
	}

	//TODO: SetTime function. The rest are built into MonoBehaviour
}
