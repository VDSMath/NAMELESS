using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour,IWeapon {

    [Header("Animation Properties")]
    [SerializeField] private float knockbackForce;
	[SerializeField] private int shootingDrag;
	[SerializeField] private float shootingSpeed;
	[SerializeField] private int shootingDamage;
	[SerializeField] private float shootingCoolDown;
	[SerializeField] private GameObject arrow;
	private bool canShoot = true;

	private void Update(){
		GetArrowDirection();
		Shoot();		
	}
	private IEnumerator ShootCoolDown(){
		canShoot = false;
		yield return new WaitForSeconds(shootingCoolDown);
		canShoot = true;
	}
	private void Shoot(){
		if(Input.GetKeyDown(KeyCode.F) && canShoot){
			CreateShoot();
			StartCoroutine( ShootCoolDown() );
		}
	}
	private void CreateShoot(){
		GameObject temp = Instantiate(arrow,transform.position,Quaternion.identity);
		temp.GetComponent<Rigidbody2D>().AddForce(shootingSpeed*transform.up);
		temp.transform.up = transform.up;
		temp.GetComponent<Rigidbody2D>().drag = shootingDrag;
		temp.GetComponent<Arrow>().SetShootingDamage(shootingDamage);
	}
	private void GetArrowDirection(){
		transform.up = (Vector3) GetComponentInParent<PlayerMovement>().lastDirection;
	}
	public int GetWeaponDamage(){
		return shootingDamage;
	}
    public float GetKnockback()
    {
        return knockbackForce;
    }
}
