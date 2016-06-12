using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	// Use this for initialization
	public GameObject magnum;
	public GameObject zod;
	private ShootScript _magnum_script;
	private ShootScript _zod_script;
	void Start () {
		_zod_script = zod.GetComponent<ShootScript>();
		_zod_script.rps = 1f;
		_magnum_script = magnum.GetComponent<ShootScript>();
		_magnum_script.rps = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)){
			magnum.SetActive(true);
			zod.SetActive(false);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			magnum.SetActive(false);
			zod.SetActive(true);
		}
	}
}
