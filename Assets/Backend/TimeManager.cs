using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

	//in seconds
	public float timeLimit = 180f;

	[SerializeField]
	private TextMeshProUGUI _timeDisplay;

	[SerializeField]
	private GameObject _resultsGroup;

	[SerializeField]
	private GameObject _gameGroup;

	private float _timer;

	void Start()
	{
		_timer = timeLimit;
	}

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        int minutes = (int)(_timer / 60);
        int seconds = (int)(_timer % 60);

        string extraZero = "";

        if(seconds < 10)
        {
        	extraZero = "0";
        }

        _timeDisplay.text = (minutes + ":" + extraZero +""+ seconds);

        if(_timer < 30)
        {
        	//turn tmp red
        	_timeDisplay.color = Color.red;
        }

        if(_timer < 0)
        {
        	//Times up!
        	_gameGroup.SetActive(false);
        	_resultsGroup.SetActive(true);
        }
    
	}	
}
