using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerInteractor : MonoBehaviour {

	[Header("Interact Preferences")]
	[SerializeField] private float interactDistance;

    [HideInInspector]
    public bool canObserve;
	private Vector2 direction;

    private void Start()
    {
        canObserve = true; 
    }

    private void Update(){
		GetNewDirection();
		InteractFromInput();
	}
	private void InteractFromInput(){
		if(Input.GetButtonDown("Fire3")){

			RaycastHit2D ray = Physics2D.Raycast(transform.position,direction,interactDistance);
			if(ray.collider != null) {
                if (ray.collider.gameObject.GetComponent<IInteractable>() != null)
                {
                    ray.collider.gameObject.GetComponent<IInteractable>().Interact();
                }

                if(ray.collider.gameObject.GetComponent<AObservable>() != null && canObserve)
                {
                    canObserve = false;
                    ray.collider.gameObject.GetComponent<AObservable>().Observe(gameObject);
                    gameObject.GetComponent<PlayerMovement>().canMove = false;
                }
			}

		}
	}
	private void GetNewDirection(){
		direction = GetComponent<PlayerMovement>().GetLastDirection();
	}
	
}
