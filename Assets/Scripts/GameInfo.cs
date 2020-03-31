using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int _score;
    int _bestScore;
    int _stars;

    public static bool bSoundOn = true;

    public static int Score
    {
        get
        {
            return PlayerPrefs.GetInt("Score");
        }
        set
        {
            int s = PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("Score", value);

            BestScore = value;
        }
    }

    public static int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt("BestScore");
        }

        set
        {
            int ss = PlayerPrefs.GetInt("BestScore", 0);
            if (ss < value)
            {
                PlayerPrefs.SetInt("BestScore", value);
            }
        }
    }

    public static int Stars
    {
        get
        {
            return PlayerPrefs.GetInt("Stars", 0);
        }

        set
        {
            PlayerPrefs.SetInt("Stars", value);
        }
    }
}
