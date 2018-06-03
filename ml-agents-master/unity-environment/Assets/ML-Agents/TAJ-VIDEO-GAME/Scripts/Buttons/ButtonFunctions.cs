using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

    public GameObject primaryWindow;
	public GameObject secondaryWindow;
	private GameObject playerSelectionWindow;

    public void Awake()
    {
		playerSelectionWindow = GameObject.FindGameObjectWithTag ("PlayerSelectionWindow");
		playerSelectionWindow.SetActive (false);
        secondaryWindow.SetActive(false);


    }

	public void OpenPlayerSelectionWindow(){
        primaryWindow.SetActive(false);
        playerSelectionWindow.SetActive(true);
        GameObject.Find("ITTLA").GetComponent<Text>().text = "<color=#60C4FFFF>ITTLA</color>";
        GameObject.Find("NERTA").GetComponent<Text>().text = "<color=#FFFFFFFF>NERTA</color>";
        PlayerSelection.playerFactionChosen = PlayerSelection.PlayerSelections.Ittla;


        GameObject.Find("EASY").GetComponent<Text>().text = "<color=#60C4FFFF>EASY</color>";
        GameObject.Find("MEDIUM").GetComponent<Text>().text = "<color=#FFFFFFFF>MEDIUM</color>";
        GameObject.Find("HARD").GetComponent<Text>().text = "<color=#FFFFFFFF>HARD</color>";
        PlayerSelection.playerDifficultyChosen = PlayerSelection.Difficulty.Easy;
        GetComponent<AudioSource>().Play();
	}
	public void ClosePlayerSelectionWindow(){
        GameObject.Find("ITTLA").GetComponent<Text>().text = "<color=#60C4FFFF>ITTLA</color>";
        GameObject.Find("NERTA").GetComponent<Text>().text = "<color=#FFFFFFFF>NERTA</color>";
        PlayerSelection.playerFactionChosen = PlayerSelection.PlayerSelections.Ittla;


        GameObject.Find("EASY").GetComponent<Text>().text = "<color=#60C4FFFF>EASY</color>";
        GameObject.Find("MEDIUM").GetComponent<Text>().text = "<color=#FFFFFFFF>MEDIUM</color>";
        GameObject.Find("HARD").GetComponent<Text>().text = "<color=#FFFFFFFF>HARD</color>";
        PlayerSelection.playerDifficultyChosen = PlayerSelection.Difficulty.Easy;

		playerSelectionWindow.SetActive (false);
        primaryWindow.SetActive(true);
        GetComponent<AudioSource>().Play();
	}

    public void GoToPlayScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenSecondaryWindow()
    {
        primaryWindow.SetActive(false);
        secondaryWindow.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
    public void CloseSecondaryWindow()
    {
        secondaryWindow.SetActive(false);
        primaryWindow.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void ResumeGame()
    {
        CloseSecondaryWindow();
    }
    public void PauseGame()
    {
        OpenSecondaryWindow();
    }

    public void ExitToIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void selectIttlaAsPlayer(){
        PlayerSelection.playerFactionChosen = PlayerSelection.PlayerSelections.Ittla;
        GameObject.Find("ITTLA").GetComponent<Text>().text = "<color=#60C4FFFF>ITTLA</color>";
        GameObject.Find("NERTA").GetComponent<Text>().text = "<color=#FFFFFFFF>NERTA</color>";
        GetComponent<AudioSource>().Play();
	}

	public void selectNertaAsPlayer(){
        PlayerSelection.playerFactionChosen = PlayerSelection.PlayerSelections.Nerta;
        GameObject.Find("NERTA").GetComponent<Text>().text = "<color=#FF0064FF>NERTA</color>";
        GameObject.Find("ITTLA").GetComponent<Text>().text = "<color=#FFFFFFFF>ITTLA</color>";
        GetComponent<AudioSource>().Play();
	}

    public void selectEasyAsDifficulty()
    {
        Debug.Log("EASY");
        PlayerSelection.playerDifficultyChosen = PlayerSelection.Difficulty.Easy;
        GameObject.Find("EASY").GetComponent<Text>().text = "<color=#60C4FFFF>EASY</color>";
        GameObject.Find("MEDIUM").GetComponent<Text>().text = "<color=#FFFFFFFF>MEDIUM</color>";
        GameObject.Find("HARD").GetComponent<Text>().text = "<color=#FFFFFFFF>HARD</color>";
        GetComponent<AudioSource>().Play();
    }

    public void selectMediumAsDifficulty()
    {
        Debug.Log("MEDIUM");
        PlayerSelection.playerDifficultyChosen = PlayerSelection.Difficulty.Medium;
        GameObject.Find("EASY").GetComponent<Text>().text = "<color=#FFFFFFFF>EASY</color>";
        GameObject.Find("MEDIUM").GetComponent<Text>().text = "<color=#60C4FFFF>MEDIUM</color>";
        GameObject.Find("HARD").GetComponent<Text>().text = "<color=#FFFFFFFF>HARD</color>";
        GetComponent<AudioSource>().Play();
    }
    public void selectHardAsDifficulty()
    {
        Debug.Log("HARD");
        PlayerSelection.playerDifficultyChosen = PlayerSelection.Difficulty.Hard;
        GameObject.Find("EASY").GetComponent<Text>().text = "<color=#FFFFFFFF>EASY</color>";
        GameObject.Find("MEDIUM").GetComponent<Text>().text = "<color=#FFFFFFFF>MEDIUM</color>";
        GameObject.Find("HARD").GetComponent<Text>().text = "<color=#60C4FFFF>HARD</color>";
        GetComponent<AudioSource>().Play();
    }
}
