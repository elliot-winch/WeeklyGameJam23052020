using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ResultsScreen : MonoBehaviour
{
	public float animationInterval = .02f;

	[SerializeField]
	private GameObject _wealthMeter;

	[SerializeField]
	private GameObject _reputationMeter;

	[SerializeField]
	private GameObject _loyaltyMeter;

	[SerializeField]
	private Button _playAgainButton;

	private int _wealthVal = 30;
	private int _reputationVal = 55;
	private int _loyaltyVal = 20;


    // Start is called before the first frame update
    void OnEnable(){

    	StartCoroutine(MeterDisplay());
    	_playAgainButton.onClick.AddListener(RestartGame);

    }



    IEnumerator MeterDisplay(){

    	float currentFill = 0;
    	Image wealthIm = _wealthMeter.GetComponentInChildren<Image>();
    	_wealthMeter.SetActive(true);
    	wealthIm.fillAmount = 0;

    	while(currentFill < (float)_wealthVal / (100)){
    		yield return new WaitForSeconds(animationInterval);
    		currentFill += .01f;
    		wealthIm.fillAmount = currentFill;
    	}


    	currentFill = 0;
    	Image reputationIm = _reputationMeter.GetComponentInChildren<Image>();
    	_reputationMeter.SetActive(true);
    	reputationIm.fillAmount = 0;

    	while(currentFill < (float)_reputationVal / (100)){
    		yield return new WaitForSeconds(animationInterval);
    		currentFill += .01f;
    		reputationIm.fillAmount = currentFill;
    	}


    	currentFill = 0;
    	Image loyaltyIm = _loyaltyMeter.GetComponentInChildren<Image>();
    	_loyaltyMeter.SetActive(true);
    	loyaltyIm.fillAmount = 0;

    	while(currentFill < (float)_loyaltyVal / (100)){
    		yield return new WaitForSeconds(animationInterval);
    		currentFill += .01f;
    		loyaltyIm.fillAmount = currentFill;
    	}

    }

    private void RestartGame(){
    	SceneManager.LoadScene("GameScene");
    }

}
