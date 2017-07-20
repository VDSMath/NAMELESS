using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AObservable : MonoBehaviour {

    protected GameObject cam;
    protected bool canMove, moveBack;
    protected float journeyLength, startTime;
    private Vector3 iniz;

    [SerializeField]
    protected float moveSpeed;

    public void Observe()
    {
        cam = Camera.main.gameObject;
        iniz = cam.transform.position;
        startTime = Time.time;
        journeyLength = Vector2.Distance(cam.transform.position, transform.position);
        Debug.Log(journeyLength);
        canMove = true;
    }

    
    protected void MoveBack()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector2.Lerp(this.transform.position, cam.transform.position, fracJourney);

        if (fracJourney >= 1)
        {
            moveBack = false;
        }
    }

    protected void Move()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector2.Lerp(cam.transform.position, this.transform.position, fracJourney);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, iniz.z);
        if (fracJourney >= 1)
        {
            canMove = false;
        }
    }
}
