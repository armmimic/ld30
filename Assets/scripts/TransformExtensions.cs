using UnityEngine;

public static class TransformExtensions{
	public static Transform FindRecursive(this Transform node, string name){
		if(node.name == name){
			return node;
		}

		foreach(Transform child in node){
			Transform t = child.FindRecursive(name);
			if(t != null){
				return t;
			}
		}

		return null;
	}
}
