using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Debris.CoreSolution
{
    public class BallSelectButtonCS : MonoBehaviour
    {

        // Use this for initialization
        public bool SelectedBall;
        public int Cost;
        public int ButtonID;
        bool unlockedbal = false;
        public void Start()
        {
            if (Cost == 0)
            {
                transform.GetChild(0).GetComponent<Text>().text = "";
            }
            else
                transform.GetChild(0).GetComponent<Text>().text = Cost.ToString();

            SelectedBall = (PlayerPrefs.GetInt("SelectedBall", 0) == ButtonID) ? true : false;

             unlockedbal = (PlayerPrefs.GetInt("UnlockedBall" + ButtonID, 0) == 1) ? true : false;

            transform.GetChild(1).gameObject.SetActive(SelectedBall);

            if (ButtonID == 0 || unlockedbal ) // PlayerPrefs.GetInt("Stars", 0) >= Cost)
            {
                transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClickButton()
        {
            if (unlockedbal)
            {
                PlayerPrefs.SetInt("SelectedBall", ButtonID);
                PlayerPrefs.Save();
                SoundManager.instance.PlaySound(Constants.BALLSELECT);
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    transform.parent.GetChild(i).GetComponent<BallSelectButtonCS>().Start();
                }

                return;
            }
            if (GameInfo.Stars >= Cost)
            {
                PlayerPrefs.SetInt("UnlockedBall" + ButtonID, 1);
                PlayerPrefs.SetInt("SelectedBall", ButtonID);
                unlockedbal = true;
                PlayerPrefs.Save();
                GameInfo.Stars -= Cost;
                SoundManager.instance.PlaySound(Constants.BALLSELECT);
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    transform.parent.GetChild(i).GetComponent<BallSelectButtonCS>().Start();
                }
            }
        }

       
    }
}
