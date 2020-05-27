using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ResultsScreen : MonoBehaviour
{
	public float animationInterval = .08f;

	[SerializeField]
	private Image _wealthMeter;

	[SerializeField]
	private Image _reputationMeter;

	[SerializeField]
	private Image _loyaltyMeter;

	[SerializeField]
	private Button _playAgainButton;

	[SerializeField]
	private TextMeshProUGUI _cultRanking;

	[SerializeField]
	private TextMeshProUGUI _cultEvaluation;

	private int _wealthVal = 90;
	private int _reputationVal = 75;
	private int _loyaltyVal = 70;
	private int _valMax = 100;

	private bool _evaluate;


    // Start is called before the first frame update
    void OnEnable(){

    	_playAgainButton.onClick.AddListener(RestartGame);

    	//enter welath, reputation , and lyalty vals here

    	StartCoroutine(MeterDisplay(_wealthMeter, _wealthVal));
    	StartCoroutine(MeterDisplay(_reputationMeter, _reputationVal));
    	StartCoroutine(MeterDisplay(_loyaltyMeter, _loyaltyVal));

    }

    void Update()
    {
    	
    }

    IEnumerator MeterDisplay(Image meter, int val) {
    	float currentFill = 0;
    	meter.fillAmount = 0;

    	while(currentFill < (float)val / (_valMax)){
    		yield return new WaitForSeconds(animationInterval);
    		currentFill += .01f;
    		meter.fillAmount = currentFill;
    	}
    	if(!_evaluate){
    		EvaluationDisplay();
    		_evaluate = true;

    	}

    }


    private void EvaluationDisplay(){

    	float score = ((float)_wealthVal + (float)_reputationVal + (float)_loyaltyVal) / (float)(_valMax * 3);

    	Debug.Log(score);

    	_cultRanking.gameObject.SetActive(true);
    	_cultEvaluation.gameObject.SetActive(true);

    	if(score < .33f){
    		_cultRanking.text = "Cult Ranking: Pathetic";
    		_cultEvaluation.text = "Your cult is pathetic. Your cult members will probably revolt against your authority at some point, and then you'll have to get a real job.";
    	}

    	else if(score < .66f){
    		_cultRanking.text = "Cult Ranking: Mediocre";
    		_cultEvaluation.text = "Your cult is mediocre. If you're lucky, maybe someone will make a podcast about it, but it's definitely not going down in history as one of the 'greats'.";

    	}

    	else{
    		_cultRanking.text = "Cult Ranking: Legendary";
    		_cultEvaluation.text = "Your cult is legendary. Your members are so brainwashed that they'll still speak highly of you in the documentaries that are made after your death.";

    	}


    }



    private void RestartGame(){
    	SceneManager.LoadScene("GameScene");
    }

}
