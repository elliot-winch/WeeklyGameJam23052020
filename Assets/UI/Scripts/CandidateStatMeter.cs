using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Place on a Candidate Stat Meter (ex: Wealth). 

public class CandidateStatMeter : MonoBehaviour
{
	[SerializeField]
	private Image _positiveMeter;

	[SerializeField]
	private Image _negativeMeter;

	[SerializeField]
	private GameObject _negativeAxis;

	[SerializeField]
	private GameObject _positiveAxis;


    public void SetCandidateStats(int level, int max){
    	
    	_positiveAxis.SetActive(false);
    	_negativeAxis.SetActive(false);

    	_positiveMeter.fillAmount = 0;
    	_negativeMeter.fillAmount = 0;

    	if(level < 0){
    		_negativeAxis.SetActive(true);
    		_negativeMeter.fillAmount = ((float)level / (float)max);
    	}
    	else{
    		_positiveAxis.SetActive(true);
    		_positiveMeter.fillAmount = ((float)level / (float)max);
    	}
    }
}
