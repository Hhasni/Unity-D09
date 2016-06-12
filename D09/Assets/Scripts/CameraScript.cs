using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	public Transform 			target;
	public float 				runDistance;
	public float 				height;
	private Transform 			_myTransform;
	// Use this for initialization
	void Start () {
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LateUpdate () {
		_myTransform.position = new Vector3 (target.position.x, target.position.y + height, target.position.z - runDistance);
		_myTransform.LookAt (target);
	}
}
