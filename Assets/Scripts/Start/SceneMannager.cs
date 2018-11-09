using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Developed by alejo9604
*/


/* Static Scene manager*/
public class SceneMannager : MonoBehaviour {

	public static SceneMannager SM;

	public List<Texture> skins;

	private const string SceneStart = "Start";
	private const string SceneGame = "Game";
	private const string SceneEnd = "End";

	void Awake()
	{
		SM = this;
		DontDestroyOnLoad(gameObject);

		for (int i = 1; i <= 3; i++)
		{
			if (!PlayerPrefs.HasKey("Rank" + i))
			{
				PlayerPrefs.SetInt("Score" + i, 0);
				PlayerPrefs.SetString("Rank" + i, string.Empty);
			}
		}
	}


	public void LoadStart()
	{
		SceneManager.LoadScene(SceneStart);		
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(SceneGame);
	}

	public void LoadEnd(int score)
	{
		setRank(score);
		SceneManager.LoadScene(SceneEnd);
	}

	/* Load - Save Rank */
	void setRank(int score)
	{
		string name = string.Empty;
		if (PlayerPrefs.HasKey("User"))
			name = PlayerPrefs.GetString("User");
		
		int Scorei = 0;
		string Namesi = string.Empty;

		for (int i = 1; i <= 3; i++)
		{
			if (PlayerPrefs.HasKey("Rank" + i))
			{
				Namesi= PlayerPrefs.GetString("Rank" + i);
				Scorei = PlayerPrefs.GetInt("Score" + i);

				if (score >= Scorei)
				{
					PlayerPrefs.SetInt("Score" + i, score);
					PlayerPrefs.SetString("Rank" + i, name);
					score = Scorei;
					name = Namesi;
				}
			}
		}
	}
}
