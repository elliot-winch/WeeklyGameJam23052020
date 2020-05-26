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

	private float _timer;

	void Start(){
		_timer = timeLimit;
	}

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        Debug.Log((int)(_timer / 60));


    }
}
