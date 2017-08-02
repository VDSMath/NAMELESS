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
	[SerializeField] private int actualArmor;
	[SerializeField] private float iFrameTimeIfHitted;
	[SerializeField] private Slider sliderHP;
	[SerializeField] private Image armorImage;

	private bool canTakeDamage;

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

    [Header("Items")]
    [SerializeField] private float defenseBuffDuration;
    public int numberOfCures;
    public int numberOfDefenseBuffs;
    public int numberOfKeys;   
    [SerializeField] private Text cureCounter;
    [SerializeField] private Text defenseCounter;
    [SerializeField] private Text keyCounter;

    private bool buffedDefense;

    private void Start(){
		ShowMainItem();
		canTakeDamage = true;
        buffedDefense = false;
		sliderHP.maxValue = maxLife;
		sliderENE.maxValue = maxEnergy;
	}
	private void Update(){
		ChangeMainItem();
        UsePickup();
	}

    private void UsePickup()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && numberOfCures >= 1)
        {
            numberOfCures--;
            cureCounter.text = numberOfCures.ToString();
            sliderHP.value = actualLife += maxLife / 2;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && numberOfDefenseBuffs >= 1)
        {
            numberOfDefenseBuffs--;
            defenseCounter.text = numberOfDefenseBuffs.ToString();
            StartCoroutine(ActivateDefenseBuff());
        }
    }

    private IEnumerator ActivateDefenseBuff()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        buffedDefense = true;
        yield return new WaitForSeconds(defenseBuffDuration);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        buffedDefense = false;
    }

    public void PickUp(string item)
    {
        switch (item)
        {
			case "armor":
				actualArmor++;
				RefreshArmor();
				break;

            case "cure":
                numberOfCures++;
                cureCounter.text = numberOfCures.ToString();
                break;

            case "defense":
                numberOfDefenseBuffs++;
                defenseCounter.text = numberOfDefenseBuffs.ToString();
                break;

            case "key":
                ++numberOfKeys;
                keyCounter.text = numberOfKeys.ToString();
                break;
        }
    }

	private void RefreshArmor() 
	{
		if(actualArmor >= 1) {
			armorImage.gameObject.SetActive(true);
			Text t = armorImage.GetComponentInChildren<Text>();
			t.text = actualArmor.ToString();
		} else {
			armorImage.gameObject.SetActive(false);
		}
	}

    public void TakeDamage(GameObject dealer, float knockbackDistance, int damageAmount)
    {
		if (canTakeDamage) {
			Vector2 knockbackDirection = dealer.transform.position - transform.position;
			GetComponent<Rigidbody2D>().AddForce(-knockbackDirection.normalized * knockbackDistance);
			StartCoroutine(Flash());

			if (buffedDefense)
				damageAmount -= damageAmount / 2;

			if (actualArmor >= 1) {
				actualArmor -= damageAmount;
				if (actualArmor < 0)
					actualArmor = 0;

				RefreshArmor();
			} else {
				actualLife -= damageAmount;
			}

			if (actualLife - 1 >= 0) {
				sliderHP.value = actualLife;
			} else {
				GameOver();
			}
		}
    }

	private IEnumerator Flash() {
		canTakeDamage = false;

		Color atual = gameObject.GetComponent<SpriteRenderer>().color;
		Color transparente = atual;
		transparente.a = 0.2f;

		int flashLoops = 8;

		gameObject.GetComponent<SpriteRenderer>().color = transparente;
		yield return new WaitForSeconds(0.3f);

		for (int i = 0; i <= flashLoops; i++) {
			if (i % 2 == 1)
				gameObject.GetComponent<SpriteRenderer>().color = transparente;
			else
				gameObject.GetComponent<SpriteRenderer>().color = atual;
			
			yield return new WaitForSeconds(.1f);
		}

		canTakeDamage = true;
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
