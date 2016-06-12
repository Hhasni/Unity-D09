using UnityEngine;
using System.Collections;

public class mapTriggerScipt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerExit(Collider col){
		Destroy (col.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
