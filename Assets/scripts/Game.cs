using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public Elevator elevator;
	public Transform spawnPos;
	public GUIText text;

	private Location currLocation;
	private bool arriving;

	private string input;

	// Use this for initialization
	void Start () {
		text.text = "";

		currLocation = GenerateLocation("rgb");
		StartCoroutine(ArriveAtLocation(currLocation));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			input += Symbol.Red;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			input += Symbol.Green;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			input += Symbol.Blue;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			bool passHas = elevator.PassengerHasLocation(input);
			if(passHas && !elevator.IsPendingLocation(input)){
				Debug.Log("correct location "+input);
				elevator.pendingLocations.Add(input);
			} else {
				Debug.Log("nope "+passHas+input);
			}

			input = "";
		}



		if(!arriving){
			if(elevator.pendingLocations.Count > 0){
				string code = elevator.pendingLocations[0];
				elevator.pendingLocations.RemoveAt(0);

				currLocation = GenerateLocation(code);
				StartCoroutine(ArriveAtLocation(currLocation));
			}
		}
	}

	public IEnumerator ArriveAtLocation(Location location){
		arriving = true;

		Debug.Log("Arrive at "+location.code+" passengers: "+elevator.passengers.Count + " waiting: "+location.passengers.Count);

		List<Person> peopleToRemove = new List<Person>();
		// check for people who need to leave
		foreach(Person p in elevator.passengers){
			if(p.destination == location.code){
				peopleToRemove.Add(p);

				yield return StartCoroutine(PassengerExit(p));
			}
		}

		foreach(Person p in peopleToRemove){
			elevator.passengers.Remove(p);
			Destroy(p.gameObject);
		}

		foreach(Person p in location.passengers){
			yield return StartCoroutine(PassengerEnter(p));
		}

		arriving = false;
	}

	public IEnumerator PassengerEnter(Person p){
		elevator.passengers.Add(p);
		p.targetPos = elevator.entrance.position;

		while(!p.IsAtTargetPos()){
			yield return null;
		}

		AnnounceDestination(p);

		yield return new WaitForSeconds(2f);

		text.text = "";

		p.targetPos = elevator.FindEmptyPosition();
	}

	public IEnumerator PassengerExit(Person p){
		Debug.Log(p+" is exiting");

		p.targetPos = spawnPos.position;
		while(!p.IsAtTargetPos()){
			yield return null;
		}
	}

	public void AnnounceDestination(Person p){
		Debug.Log(p + " wants to go to " + p.destination);
		text.text = p.destination;
	}

	public Location GenerateLocation(string code){
		Location location = new Location();
		location.code = code;

		for(int i = 0; i < Random.Range(1, 10); i++){
			Person p = CreatePerson(spawnPos.position);

			location.passengers.Add(p);
		}

		return location;
	}

	public Person CreatePerson(Vector3 pos){
		GameObject personGO = Instantiate(PrefabManager.instance.person) as GameObject;

		Person person = personGO.GetComponent<Person>();
		person.destination = GenerateLocationCode();
		person.targetPos = null;

		person.transform.position = pos;

		return person;
	}

	public string GenerateLocationCode(){
		string code = "";
		for(int i = 0; i < 3; i++){
			switch(Random.Range(0, 3)){
				case 0: code += Symbol.Red; break;
				case 1: code += Symbol.Blue; break;
				default: code += Symbol.Green; break;
			}
		}
		return code;
	}

	void OnGUI(){
		GUILayout.Label(input);
	}
}
