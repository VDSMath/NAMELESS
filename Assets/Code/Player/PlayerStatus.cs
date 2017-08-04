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
	[SerializeField] private Image lifeImage;
	[SerializeField] private Image armorImage;

	private bool canTakeDamage;

	[Header("Energy Status")]
	[SerializeField] private int maxEnergy;
	[SerializeField] private int actualEnergy;
	[SerializeField] private float timeToRecover;
	[SerializeField] private Image energyImage;

	[Header("Player Mode")]
	[SerializeField] MainItems mainItem;
	[SerializeField] GameObject sword;
	[SerializeField] GameObject arrow;
	[SerializeField] Image i_sword;
	[SerializeField] Image i_arrow;

    [Header("Items")]
    [SerializeField] private GameObject bomb;
    [SerializeField] private float defenseBuffDuration;
    [SerializeField] private Text cureCounter;
    [SerializeField] private Text defenseCounter;
    [SerializeField] private Text keyCounter;

    private int numberOfBombs;
    private int numberOfCures;
    private int numberOfDefenseBuffs;
    private int numberOfKeys;

    private bool buffedDefense;
    private bool killed;

    private void Start(){
		ShowMainItem();
		canTakeDamage = true;
        buffedDefense = false;
        killed = false;
		lifeImage.fillAmount = 1;
		energyImage.fillAmount = 1;
        armorImage.fillAmount = 0;      
    }
	private void Update(){
        if (!killed)
        {
            MatarSalasExtras();
            killed = true;
        }

        ChangeMainItem();
        UsePickup();
	}

    private void UsePickup()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && numberOfCures >= 1)
        {
            numberOfCures--;
            cureCounter.text = numberOfCures.ToString();
            actualLife += maxLife / 2;
            lifeImage.fillAmount = (float)actualLife / maxLife ;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && numberOfDefenseBuffs >= 1)
        {
            numberOfDefenseBuffs--;
            defenseCounter.text = numberOfDefenseBuffs.ToString();
            StartCoroutine(ActivateDefenseBuff());
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && numberOfBombs >= 1)
        {
            GameObject b;
            numberOfBombs--;
            b = Instantiate(bomb);
            b.GetComponent<Bomb>().Explode();
            b.transform.position = transform.position;

        }
    }

    private void MatarSalasExtras()
    {
        GameObject[] extras = GameObject.FindGameObjectsWithTag("Minimap Image");

        foreach(GameObject img in extras)
        {
            Destroy(img.transform.parent.gameObject);
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

            case "bomb":
                numberOfBombs++;
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
        armorImage.fillAmount = (float)actualArmor / 3;
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
                lifeImage.fillAmount = (float)actualLife/maxLife ;
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
            --actualEnergy;
            energyImage.fillAmount = (float)actualEnergy /maxEnergy;
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
        ++actualEnergy;
        energyImage.fillAmount = (float)actualEnergy / maxEnergy;
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
