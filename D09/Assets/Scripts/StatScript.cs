using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour {


	public enum Type{Maya, Zombie};
	public Type 		type;
	public float		Strengh;
	public float		Agility;
	public float		Constitution;
	public float		Armor;
	public int			HP;
	public float		MaxHp;
	public bool			dead;
	public int			XP;
	[HideInInspector]public float		minDamage;
	[HideInInspector]public float		maxDamage;
	[HideInInspector]public float		Level;
	public float		Money;

	// Use this for initialization
	void Awake () {
		Level = 1;
		HP = Mathf.RoundToInt(5 * Constitution);
		MaxHp = Mathf.RoundToInt (HP);
		minDamage = Strengh / 2;
		maxDamage = minDamage + 4;
		
		if (Armor > 170)
			Armor = 170;
	}

	public float ft_ChanceOfHit(StatScript target){
		return (75 + Agility - target.Agility);
	}

	float ft_BaseDamage(){
		return (Random.Range (minDamage, maxDamage));
	}

	public int ft_FinalDamage(StatScript target){
		return  Mathf.RoundToInt((ft_BaseDamage() * (1 - target.Armor/200)));
	}

	// Update is called once per frame
	void Update () {
		if (HP < 0){
			HP = 0;
			dead = true;
		}
	
	}
}
