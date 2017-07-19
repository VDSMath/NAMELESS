using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IWeapon {
	private float shootingDamage;

	public void SetShootingDamage(float shootingD){
		shootingDamage = shootingD;
	}
	public float GetWeaponDamage(){
		return shootingDamage;
	}
}
