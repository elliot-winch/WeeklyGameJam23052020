using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

	[SerializeField]
	private Button _playButton;


    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(PlayClicked);
    }

    private void PlayClicked(){
    	SceneManager.LoadScene("GameScene");
    }

    
}
