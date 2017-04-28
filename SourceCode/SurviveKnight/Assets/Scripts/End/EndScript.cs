using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Developed by alejo9604
*/


/* End Scene script*/

public class EndScript : MonoBehaviour {

	public SkinnedMeshRenderer player;

	public List<Text> Names;
	public List<Text> Scores;

	/* Get and Set rank */
	void Start () {
		int skin = 0;
		if (PlayerPrefs.HasKey("Skin"))
			skin = PlayerPrefs.GetInt("Skin");
		
		player.material.SetTexture("_MainTex", SceneMannager.SM.skins[skin]);

		for (int i = 1; i <= 3; i++)
		{
			if (PlayerPrefs.HasKey("Rank" + i))
				SetRankin(i - 1, PlayerPrefs.GetString("Rank" + i), PlayerPrefs.GetInt("Score" + i).ToString());
			else
				SetRankin(i - 1, " ", " ");
		}

	}


	void SetRankin(int i, string playerName, string score)
	{
		Names[i].text = playerName;
		Scores[i].text = score;
	}

	/* Restart game */
	public void Restart()
	{
		SceneMannager.SM.LoadStart();
	}

}
