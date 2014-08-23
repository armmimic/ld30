using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {
	public static PrefabManager instance;

	public GameObject person;

	public void Awake(){
		instance = this;
	}
}
