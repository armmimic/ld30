using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour {
	public List<Person> passengers;
	public List<string> pendingLocations;

	public Transform entrance;
	public Vector3 min;
	public Vector3 max;

	void Awake(){
		entrance = transform.FindRecursive("entrance");
	}

	public Vector3 FindEmptyPosition(){
		Vector3 pos;
		pos.x = Random.Range(min.x, max.x);
		pos.y = Random.Range(min.y, max.y);
		pos.z = Random.Range(min.z, max.z);
		return pos;
	}

	public bool PassengerHasLocation(string location){
		foreach(Person p in passengers){
			//Debug.Log("pass has "+p.desiredLocation+ " "+location);
			if(p.destination == location){
				return true;
			}
		}

		return false;
	}

	public bool IsPendingLocation(string location){
		return pendingLocations.Contains(location);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube((min + (max-min)*.5f), max-min);
	}
}
