using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsHandler : MonoBehaviour {

	public Button NewGameButton, ExitButton;
	
	// Use this for initialization
	void Start ()
	{
		NewGameButton.onClick.AddListener(NewGameButtonHandler);
		ExitButton.onClick.AddListener(ExitButtonHandler);
		
	}

	void NewGameButtonHandler()
	{
		Application.LoadLevel("GameScene");
	}
	
	void ExitButtonHandler()
	{
		Application.Quit();
	}
}
