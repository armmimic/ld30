using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	public string destination;
	public Vector3? targetPos;
	public float speed;

	public bool IsAtTargetPos(){
		if(targetPos != null){
			return transform.position == targetPos;
		}

		return false;
	}

	void Update(){
		if(targetPos != null){
			if(transform.position != targetPos){
				transform.position = Vector3.MoveTowards(transform.position, targetPos.Value, speed * Time.deltaTime);
			}
		}
	}
}
