using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerSword : MonoBehaviour, IWeapon {

	[Header("Animation Properties")]
	[SerializeField] private int attackDuration;
	[SerializeField] private float attackSpeed;
	[SerializeField] private int swordDamage;
    [SerializeField] private float knockbackForce;
	private bool attacking;

	private void Update(){
		GetSwordDirection();
		Attack();		
	}
	private IEnumerator AttackAnimation(){
		attacking = true;
		for(int i = 0;i<attackDuration;i++){
			transform.Rotate(0,0,-attackSpeed*Time.deltaTime);
		}
		for(int i = 0;i<2*attackDuration;i++){
			transform.Rotate(0,0,attackSpeed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		attacking = false;
	}
	private void Attack(){
		if(Input.GetButtonDown("Fire2") && GetComponentInParent<PlayerStatus>().CanAct()){
			StartCoroutine( AttackAnimation() );
			GetComponentInParent<PlayerStatus>().LoseEnergy();
		}
	}
	private void GetSwordDirection(){
		if(!attacking){
			transform.up = (Vector3) GetComponentInParent<PlayerMovement>().lastDirection;
		}
	}
	public int GetWeaponDamage(){
		return swordDamage;
	}
    
    public float GetKnockback()
    {
        return knockbackForce;
    }
}
