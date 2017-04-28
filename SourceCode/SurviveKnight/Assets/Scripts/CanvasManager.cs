using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public static CanvasManager CM;


	[Header("Player Life")]
	public Image LifeBar;

	[Header("Score")]
	public Text ScoreText;
	private int score;

	[Header("Bullets")]
	public Image BulletImg;
	public Text AmmunitionText;

	void Awake()
	{
		CM = this;
	}

	void Start () {
		score = 0;
		setScore(0);
	}
	

	void Update () {
		
	}

	public void setScore(int points)
	{
		score += points;
		ScoreText.text = score.ToString();
	}

	public void setPlayerLife(float life){
		LifeBar.fillAmount = life;
	}

	public void setBullets(Color color, int Ammunition)
	{
		BulletImg.color = color;
		AmmunitionText.text = "x" + Ammunition;
	}
}
