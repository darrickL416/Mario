using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

	private Text coinTextScore;
	private AudioSource audioManager;
	private int scoreCount = 0;

	void Awake()
	{
		audioManager = GetComponent<AudioSource>();
	}

	void Start()
	{
		coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
		coinTextScore.text = "";
	}


    private void Update()
    {
		
	}



    void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == MyTags.COIN_TAG)
		{
			audioManager.Play();
			target.gameObject.SetActive(false);

			coinTextScore.text = "" + scoreCount;
			scoreCount = scoreCount + 1;





		}
	}

} // class

