﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    struct Direction
    {
        public bool zero;
        public Vector2 dir;
        public string d;

        public Direction(bool z, Vector2 di, string dee)
        {
            this.zero = z;
            this.dir = di;
            d = dee;
        }

    }

    public float dashCooldown, 
                 dashSpeed,
                 moveSpeed;
    private bool dashed;

    public int attackMode; //0 = Shoot, 1 = Slash

    private Vector2 shotDirection;
    public GameObject bullet;
    public float bulletCooldown,
                 bulletDamage,
                 bulletDestroyTime,
                 bulletSpeed,
                 playerEffect,
                 swordCooldown,
                 swordDamage;
    private float bulletTime,
                  swordTime;

    public GameObject sword;

    public AudioSource pew;
    Animator animHead;
    bool changedView;

    [SerializeField]
    private float maxHP;
    private float currentHP;

    // Use this for initialization
    void Start()
    {
        animHead = transform.GetChild(0).GetComponent<Animator>();
        attackMode = 0;
        swordTime = 0;
        bulletTime = 0;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationHandler();
        Move();
        AttackMode();
        Attack();
    }

    public void TakeDamage(GameObject dealer, float knockbackDistance, int damageAmount)
    {
        Vector2 knockbackDirection = dealer.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * knockbackDistance);
        currentHP -= damageAmount;

        if(currentHP <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void AttackMode()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            attackMode = (attackMode + 1) % 2;
        }
    }

    private void Attack()
    {
        swordTime -= Time.deltaTime;
        bulletTime -= Time.deltaTime;

        switch (attackMode)
        {
            case 0:
                if (bulletTime <= 0)
                {
                    Shoot();
                }
                break;

            case 1:
                if (swordTime <= 0)
                {
                    StartCoroutine(Slash());
                }
                break;
        }
    }

	private IEnumerator Slash() {
		Direction d = new Direction(true, Vector2.zero, "");
		d = AttackDirection(d);

		if (!d.zero) {

			switch (d.d) {
				case ("up"):
					transform.rotation = Quaternion.Euler(Vector3.zero);
					break;
				case ("down"):
					transform.rotation = Quaternion.Euler(0, 0, 180);
					break;
				case ("right"):
					transform.rotation = Quaternion.Euler(0, 0, 270);
					break;
				case ("left"):
					transform.rotation = Quaternion.Euler(0, 0, 90);
					break;
			}

            swordTime = swordCooldown;
            sword.SetActive(true);
			yield return new WaitForSeconds(.3f);
			sword.SetActive(false);
		}
		
    }

    private void Shoot()
    {
        Direction direction = new Direction(true, Vector2.zero,"");
        direction = AttackDirection(direction);

        if (!direction.zero)
        {
            ShotRelease(direction);
        }
    }

    private Direction AttackDirection(Direction direction)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction.dir.y = 1;
            direction.zero = false;
            direction.d = "up";
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction.dir.y = -1;
            direction.zero = false;
            direction.d = "down";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction.dir.x = 1;
            direction.zero = false;
            direction.d = "right";
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction.dir.x = -1;
            direction.zero = false;
            direction.d = "left";
        }
        //ANIMACAO CABECA
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Debug.Log("OI");
            animHead.SetFloat("hor",1);
            animHead.SetFloat("ver",0);
        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            animHead.SetFloat("hor",0);
            animHead.SetFloat("ver",1);
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            animHead.SetFloat("hor",0);
            animHead.SetFloat("ver",-1);
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            animHead.SetFloat("hor",-1);
            animHead.SetFloat("ver",0);
        }
        return direction;
    }

    private void ShotRelease(Direction direction)
    {
        //pew.Play();
        bulletTime = bulletCooldown;
        GameObject b = Instantiate(bullet);
		GameObject.Destroy(b, bulletDestroyTime);
        b.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        b.transform.position = this.transform.position;
        Vector2 temp = new Vector2(bulletSpeed * direction.dir.x, bulletSpeed * direction.dir.y);
        Vector2 vel = this.GetComponent<Rigidbody2D>().velocity * playerEffect;
        b.GetComponent<Rigidbody2D>().AddForce(temp);
        b.GetComponent<Rigidbody2D>().velocity += vel;
    }

    private void Move()
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed,
                                                              Input.GetAxis("Vertical") * moveSpeed));

        if (Input.GetKeyDown(KeyCode.Space) && !dashed)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxisRaw("Horizontal"),
                                                              Input.GetAxisRaw("Vertical")) * dashSpeed);
            StartCoroutine(DashCooldown());
        }
    }

    private IEnumerator DashCooldown()
    {
        dashed = true;
        yield return new WaitForSeconds(dashCooldown);
        dashed = false;
    }
    private void AnimationHandler(){
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Animator myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("moving", !(hor == 0 && ver == 0));
        //BODY
        if(Mathf.Abs(hor) > Mathf.Abs(ver)){
            if(hor > 0){
                myAnimator.SetFloat("hor",1);
                myAnimator.SetFloat("ver",0);
                animHead.SetFloat("hor",1);
                animHead.SetFloat("ver",0);
            }else{
                myAnimator.SetFloat("hor",-1);
                myAnimator.SetFloat("ver",0);
                animHead.SetFloat("hor",-1);
                animHead.SetFloat("ver",0);
            }
        }else{
            if(ver > 0){
                myAnimator.SetFloat("hor",0);
                myAnimator.SetFloat("ver",1);
                animHead.SetFloat("hor",0);
                animHead.SetFloat("ver",1);
            }else{
                myAnimator.SetFloat("hor",0);
                myAnimator.SetFloat("ver",-1);
                animHead.SetFloat("hor",0);
                animHead.SetFloat("ver",-1);
            }
        }

    }
}
