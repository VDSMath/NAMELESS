using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mem : AObservable {

    private void Start()
    {
        
        canMove = false;
        moveBack = false;
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
        else
        {
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        startTime = Time.time;
        //        journeyLength = Vector2.Distance(cam.transform.position, this.transform.position);
        //        moveBack = true;
        //    }
        }

        if (moveBack)
        {
            MoveBack();
        }
    }
}
