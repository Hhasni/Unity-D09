using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatXpScript : MonoBehaviour {
	
	private RectTransform	rtXp;
	private Text			myText;
	public StatScript 		PlayerStat;
	public int		 		bkpXP;
	// Use this for initialization
	void Start () {
		PlayerStat = GameObject.FindGameObjectWithTag ("Player").GetComponent<StatScript> ();
		Image[] tmp = GetComponentsInChildren<Image>();
		rtXp = tmp [1].GetComponent<RectTransform> ();
		myText = GetComponentInChildren<Text> ();
		bkpXP = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		if (bkpXP != PlayerStat.XP && bkpXP < 100) {
			if (PlayerStat.XP >= 100)
				PlayerStat.XP = 100;
			myText.text = PlayerStat.XP.ToString() + " / 100";
			rtXp.localPosition = new Vector3 (- 100 + PlayerStat.XP, 0, 0);
			bkpXP = PlayerStat.XP;
			
		}
	}
}
