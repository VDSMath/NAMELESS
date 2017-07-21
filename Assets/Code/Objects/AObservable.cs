using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AObservable : MonoBehaviour {

    protected GameObject cam, observer;
    protected bool canMove, canMoveBack, moveBack;
    protected float journeyLength, startTime;
    private Vector3 iniz;

    [SerializeField]
    protected float moveSpeed;

    private void Start()
    {
        canMove = false;
        canMoveBack = false;
        moveBack = false;
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canMoveBack)
        {
            if (canMove)
            {
                canMove = false;
            }

            observer.GetComponent<PlayerMovement>().canMove = true;
            startTime = Time.time;
            journeyLength = Vector2.Distance(cam.transform.position, iniz);
            moveBack = true;
            canMoveBack = false;
        }
        

        if (moveBack)
        {
            MoveBack();
        }
    }

    public void Observe(GameObject observerT)
    {
        observer = observerT;
        observer.GetComponent<PlayerInteractor>().canObserve = false;
        cam = Camera.main.gameObject;
        iniz = cam.transform.position;
        startTime = Time.time;
        journeyLength = Vector2.Distance(iniz, transform.position);
        canMove = true;
    }

    
    protected void MoveBack()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector2.Lerp(cam.transform.position, iniz, fracJourney);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, iniz.z);
        
        

        if (fracJourney >= 1)
        {
            if(observer != null)
            {
                observer.GetComponent<PlayerInteractor>().canObserve = true;
            }
            moveBack = false;
        }
    }

    protected void Move()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector2.Lerp(cam.transform.position, this.transform.position, fracJourney);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, iniz.z);

        if (fracJourney >= 0.1)
        {
            canMoveBack = true;
        }

        if (fracJourney >= 1)
        {
            canMove = false;
        }
    }
}
