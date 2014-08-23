using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location {
	public List<Person> passengers;
	public string code;

	public Location(){
		passengers = new List<Person>();
	}
}
