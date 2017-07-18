using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        attackMode = 0;
        swordTime = 0;
        bulletTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AttackMode();
        Attack();
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
        Debug.Log(bulletTime);

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

        return direction;
    }

    private void ShotRelease(Direction direction)
    {
        pew.Play();
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
}
