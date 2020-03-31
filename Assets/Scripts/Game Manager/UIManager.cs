using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage game UI
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance = null;

        public GameObject
            gameOverMenu,
            pauseMenu,
            btnContinue,
            hud;

        public PlayerController playerController;

        public Text
            txtScore,
            txtScore1,
            txtBestScore, 
            txtBestScore1,
            txtStars1,
            txtStars;

        public Sprite[] CountDownSprites;
        // Constructor
        private UIManager() { }

        public enum GameState
        {
            Paused = 0,
            GameOver,
            Playing,
        };

        public GameState gameState;

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
            gameState = GameState.Playing;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            hud.SetActive(true);
        }
        public void Update()
        {
            txtScore.text  = GameInfo.Score.ToString();
            txtScore1.text = GameInfo.Score.ToString();
            txtBestScore.text = GameInfo.BestScore.ToString();
            txtBestScore1.text = GameInfo.BestScore.ToString();
            txtStars1.text = GameInfo.Stars.ToString();
            txtStars.text = GameInfo.Stars.ToString();
        }
        // Event click
        public void RePlayBtn_Onclick()
        {
            SoundManager.instance.PlaySound(Constants.BUTTON_SOUND);
            if (GameController.instance.bTutorial)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }

        
		
        public IEnumerator WaitToRePlay()
        {
            btnContinue.GetComponent<Animator>().Play("continuebuttonidle");
            int i = 10;
            while (i >= 0)
            {
                btnContinue.GetComponent<Image>().sprite = CountDownSprites[i];
                yield return new WaitForSeconds(1.0f);
                i--;
            }


            btnContinue.GetComponent<Animator>().enabled = true;
            btnContinue.GetComponent<Animator>().Play("continueFadeout");
            
            yield return new WaitForSeconds(1.0f);
            if (UIManager.instance.gameState == GameState.GameOver)
            {
               // Time.timeScale = 0;
            }
        }

        // Event click
        public void HomeBtn_Onclick()
        {
            SoundManager.instance.PlaySound(Constants.BackMenuSound);
            Time.timeScale = 1;

            SceneManager.LoadScene("Menu");
        }

        public void OnClickPause()
        {
            playerController.SetEnable(false);
            gameState = GameState.Paused;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            gameOverMenu.SetActive(false);
            hud.SetActive(false);
        }

        public void OnClickPlayPause()
        {
            playerController.SetEnable(true);
            gameState = GameState.Playing;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            hud.SetActive(true);
        }

        public void OnClickContinueWithStar10()
        {
            if (GameInfo.Stars < 10) return;
            Vector3 pos = playerController.transform.position;
            pos.y += 2;
            playerController.transform.position = pos;
            gameState = GameState.Playing;
            playerController.SetEnable(true);
            GameInfo.Stars -= 10;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            hud.SetActive(true);

        }

        public void GameOver()
        {
            Debug.Log("GameOver");
            playerController.SetEnable(false);
            gameState = GameState.GameOver;
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(true);
            hud.SetActive(false);

            StartCoroutine(WaitToRePlay());
        }
    }
}
