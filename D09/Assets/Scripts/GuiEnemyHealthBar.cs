using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiEnemyHealthBar : MonoBehaviour {
	
	public GameObject guiTarget;
	public StatScript TargetScript;
	private RectTransform rtLife;
	public Vector3				OriPos;
	private Image				myGui;
	public bool					isActive;
	public bool					isLock;
	private GameObject			childGui;
	// Use this for initialization
	void Start () {
		isLock = false;
		isActive = false;
		childGui = gameObject.transform.GetChild (0).gameObject;
		Image[] tmp = GetComponentsInChildren<Image>();
		rtLife = tmp [2].GetComponent<RectTransform> ();
		myGui = tmp [0];
		OriPos = rtLife.position;
	}

	public void DisableGui(){
		guiTarget = null;
		myGui.color = new Color(255,255,255,0);
		childGui.SetActive (false);
		isActive = false;
		isLock = false;
		TargetScript = null;
	}

	public void ChangeGuiTarget(GameObject gTarget, bool i){
		if (i == true)
			isLock = true;
		isActive = true;
		myGui.color = Color.white;
		childGui.SetActive (true);
		guiTarget = gTarget;
		if (gTarget)
			TargetScript = guiTarget.GetComponent<StatScript> ();
	}
	// Update is called once per frame
	void Update () {
		if (guiTarget){
			//Debug.Log(TargetScript.HP);
			rtLife.localPosition = new Vector3 (-100 + ((100 / TargetScript.MaxHp) * TargetScript.HP),
			                               0,
			                               0);
		}
	}
}
