using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMannager : MonoBehaviour {

	public static SceneMannager SM;

	private const string SceneStart = "Start";
	private const string SceneGame = "Game";

	void Awake()
	{
		SM = this;
		DontDestroyOnLoad(gameObject);
	}


	public void LoadStart()
	{
		SceneManager.LoadScene(SceneStart);		
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(SceneGame);
	}
}
