using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage main menu 
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        
        public static MenuManager instance = null;

        public Toggle SoundToggle;
        
        public Text
            bestScoreText,
            starText1,
            starText2;

        // Constructor
        private MenuManager() { }

		public string rateLink;

        // Behaviour messages
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Behaviour messages
        void Start()
        {
            bestScoreText.text = GameInfo.BestScore.ToString();
            starText1.text = GameInfo.Stars.ToString();
            starText2.text = GameInfo.Stars.ToString();

            //PlayerPrefs.SetInt("Stars", 199);
        }

        void Update()
        {
            bestScoreText.text = GameInfo.BestScore.ToString();
            starText1.text = GameInfo.Stars.ToString();
            starText2.text = GameInfo.Stars.ToString();
            
            SoundToggle.isOn = GameInfo.bSoundOn;
        }

        // Event click
        public void PlayBtn_Onclick()
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);
            SceneManager.LoadScene("Game");
        }

        public void OnClickLeaderboard()
        {

        }
        public void OnClickTutorial()
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);
            SceneManager.LoadScene("Tutorial");
        }

        public void OnClickSoundButton()
        {
            GameInfo.bSoundOn = !GameInfo.bSoundOn;
        }
        // Event click
        public void RateBtn_Onlick()
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);

			Application.OpenURL (rateLink);
        }

        
        // Event click
        public void PlayGameWithBird(int i)
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);
            //GameData.Instance.SaveData(Constants.BIRD_TYPE, i);
            SceneManager.LoadScene("Game");
        }
        public void OnClickBackToMenu()
        {
            SoundManager.instance.PlaySound(Constants.BackMenuSound);
        }

        public void OnClickBallShop()
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);
            
            
        }
		
    }
}
