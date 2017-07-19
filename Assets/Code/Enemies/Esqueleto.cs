using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esqueleto : IEnemy {

    [SerializeField]
    private GameObject player, arrow;

    [SerializeField]
    private float arrowCooldown, arrowSpeed, rangedAggroDistance;
    private float arrowC;

	// Use this for initialization
	void Start () {
        arrowC = 0;
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        CheckDistance(player);
	}

    private void CheckDistance(GameObject target)
    {
        arrowC += Time.deltaTime;

        Vector3 vectorD = target.transform.position - transform.position;
        float distance = vectorD.magnitude;
        Vector3 direction = vectorD.normalized;

        if(distance <= rangedAggroDistance && arrowC >= arrowCooldown)
        {
            Shoot(direction);
        }
    }

    private void LookAt2D(GameObject looker,GameObject target)
    {
        Vector3 diff = target.transform.position - looker.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        looker.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Shoot(Vector3 direction)
    {
        GameObject temp = Instantiate(arrow, transform.position, Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().AddForce(arrowSpeed * direction);
        temp.transform.up = transform.up;
        temp.GetComponent<Arrow>().SetShootingDamage(damage);

        LookAt2D(temp, player);

        arrowC = 0;
    }
}
