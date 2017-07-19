using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerInteractor : MonoBehaviour {

	[Header("Interact Preferences")]
	[SerializeField] private float interactDistance;
	
	private Vector2 direction;

	private void Update(){
		GetNewDirection();
		InteractFromInput();
	}
	private void InteractFromInput(){
		if(Input.GetKeyDown(KeyCode.Q)){

			RaycastHit2D ray = Physics2D.Raycast(transform.position,direction,interactDistance);
			if(ray.collider != null && ray.collider.gameObject.GetComponent<IInteractable>() != null){
				ray.collider.gameObject.GetComponent<IInteractable>().Interact();
			}

		}
	}
	private void GetNewDirection(){
		direction = GetComponent<PlayerMovement>().GetLastDirection();
	}


	
}
