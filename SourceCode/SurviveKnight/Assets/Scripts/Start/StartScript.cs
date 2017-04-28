using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Developed by alejo9604
*/


/* Start scene script manager*/
public class StartScript : MonoBehaviour {


	public SkinnedMeshRenderer player;

	public Texture defaultSkin;
	private int selectedSkin;

	public InputField Input;

	void Start () {
		selectedSkin = 0;
		player.material.SetTexture("_MainTex", defaultSkin);
	}



	public void setSkin(int skin)
	{
		if (skin < SceneMannager.SM.skins.Count)
		{
			selectedSkin = skin;
			player.material.SetTexture("_MainTex", SceneMannager.SM.skins[skin]);
		}
		else {
			selectedSkin = 0;
			player.material.SetTexture("_MainTex", defaultSkin);
		}
	}

	public void start()
	{
		if (Input.text != string.Empty)
		{
			PlayerPrefs.SetString("User", Input.text);
			PlayerPrefs.SetInt("Skin", selectedSkin);

			SceneMannager.SM.LoadGame();
		}
	}
}
