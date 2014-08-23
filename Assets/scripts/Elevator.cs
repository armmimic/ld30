using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour {
	public List<Person> passengers;
	public List<string> pendingLocations;

	public bool PassengerHasLocation(string location){
		foreach(Person p in passengers){
			//Debug.Log("pass has "+p.desiredLocation+ " "+location);
			if(p.desiredLocation == location){
				return true;
			}
		}

		return false;
	}

	public bool IsPendingLocation(string location){
		return pendingLocations.Contains(location);
	}
}
