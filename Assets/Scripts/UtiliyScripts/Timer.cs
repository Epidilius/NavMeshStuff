using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	//delegate void MultiDel();
	//MultiDel multiDel;
	//multiDel += FuncOne;
	//multiDel += funcTwo;
	//multiDel()
	//This calls both FuncOne and FuncTwo
	//multiDel -= FuncOne	//This removes functionality

	float timeLeft;
	string id;
	bool isRunning;

	public delegate void FunctionToCall();
	FunctionToCall functionToCall;

	// Use this for initialization
	public void Start () {
	
	}

	public void SetParams(string aID, float aLength, FunctionToCall aFunction) {
		Debug.Log ("Params Set");
		id = aID;
		timeLeft = aLength;
		functionToCall = aFunction;
		isRunning = true;
	}
	
	public void TimerUpdate () {
		if (isRunning == true) {
			if (timeLeft >= 0.0f) {
				//TODO: Better subtraction, try to get once per second
				timeLeft -= Time.deltaTime;
				Debug.Log ("Timer time: " + timeLeft.ToString ());
			} else {
				//TODO: Call delegate of class that created timer
				if (functionToCall != null) {
					functionToCall ();
					isRunning = false;
					//Destroy(this);
				}
			}
		} else {
			Debug.Log ("isRunning == " + isRunning.ToString());
		}
	}

	public string GetID() {
		return id;
	}
	public void SetID(string aID) {
		id = aID;
	}

	//TODO: SetNewTime 
	//TODO: SetNewFunctionToCall
	//TODO: SetNewID
	//TODO: Test pause, stop, and cancel from monobehaviour
	//TODO: If they work, remove the isRunning var
}
