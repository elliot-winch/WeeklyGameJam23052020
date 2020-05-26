using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CultStatMeter : MonoBehaviour
{

	[SerializeField]
	private Image _meter;
    
	public void SetCultStats(int level, int max){
		_meter.fillAmount = (float)level / (float)max;
	}



}
