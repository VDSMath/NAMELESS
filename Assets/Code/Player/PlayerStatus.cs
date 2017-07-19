using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum MainItems{
	SWORD, ARROW, HOOK
}
public class PlayerStatus : MonoBehaviour {
	[Header("Health Status")]
	[SerializeField] private int maxLife;
	[SerializeField] private int actualLife;
	[SerializeField] private float iFrameTimeIfHitted;
	[SerializeField] private Slider sliderHP;

	[Header("Energy Status")]
	[SerializeField] private int maxEnergy;
	[SerializeField] private int actualEnergy;
	[SerializeField] private float timeToRecover;
	[SerializeField] private Slider sliderENE;

	[Header("Player Mode")]
	[SerializeField] MainItems mainItem;
	[SerializeField] GameObject sword;
	[SerializeField] GameObject arrow;
	[SerializeField] Image i_sword;
	[SerializeField] Image i_arrow;

	private void Start(){
		ShowMainItem();
		sliderHP.maxValue = maxLife;
		sliderENE.maxValue = maxEnergy;
	}
	private void Update(){
		ChangeMainItem();
	}

    public void TakeDamage(GameObject dealer, float knockbackDistance, int damageAmount)
    {
        Vector2 knockbackDirection = dealer.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(-knockbackDirection.normalized * knockbackDistance);
        actualLife -= damageAmount;

        if (actualLife - 1 >= 0)
        {
            sliderHP.value = --actualLife;
        }
        else
        {
            GameOver();
        }
    }

    
	public void LoseEnergy(){
		if(actualEnergy-1 >= 0){
			sliderENE.value = --actualEnergy;
		}else{
			//
		}
		StopCoroutine(RecoverEnergyByTime());
		StartCoroutine(RecoverEnergyByTime());
	}
	public bool CanAct(){
		return actualEnergy > 0;
	}
	private void RecoverEnergy(){
		sliderENE.value++;
		actualEnergy = (int) sliderENE.value;
	}
	private IEnumerator RecoverEnergyByTime(){
		while(actualEnergy != maxEnergy){
			yield return new WaitForSeconds(timeToRecover);
			RecoverEnergy();
		}
		StopAllCoroutines();
	}
	public void GameOver(){
		//
	}
	private void ShowMainItem(){
		switch(mainItem){
			case MainItems.SWORD:
				i_sword.enabled = true;
				i_arrow.enabled = false;
				sword.SetActive(true);
				arrow.SetActive(false);
				break;
			case MainItems.ARROW:
				i_sword.enabled = false;
				i_arrow.enabled = true;
				sword.SetActive(false);
				arrow.SetActive(true);
				break;
			case MainItems.HOOK:
				break;
		}
	}
	private void ChangeMainItem(){
		if(Input.GetKeyDown(KeyCode.E)){
			switch(mainItem){
				case MainItems.SWORD:
					mainItem = MainItems.ARROW;
					break;
				case MainItems.ARROW:
					mainItem = MainItems.SWORD;
					break;
			}
			ShowMainItem();
		}
	}
}
