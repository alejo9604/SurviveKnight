using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartScript : MonoBehaviour {


	public SkinnedMeshRenderer player;

	public List<Texture> skins;
	public Texture defaultSkin;
	private int selectedSkin;

	public InputField Input;

	void Start () {
		selectedSkin = 0;
		player.material.SetTexture("_MainTex", defaultSkin);
	}



	public void setSkin(int skin)
	{
		if (skin < skins.Count)
		{
			selectedSkin = skin;
			player.material.SetTexture("_MainTex", skins[skin]);
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
