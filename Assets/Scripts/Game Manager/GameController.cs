using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage game logic
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Renderer m_tutorialTextMesh;

        [SerializeField]
        private string m_tutorialLayerNameTextMesh;

        private int m_colorIndex;
        private int m_bonusGemPoint;

        public static GameController instance = null;

        public Color[] colors;
        public Color CurrentColor { get; set; }

        // Constructor
        private GameController() { }

        // Behaviour messages

        public GameObject[] walls;
        public GameObject playerBall;

        GameObject[] wallobjs;
        int[][] SceenOrder;
        int[] SceenNumbers;
        int Level = 0;

        int TotWalls = 30;
        public bool bTutorial = false;
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
            SceenOrder = new int[6][];
            SceenNumbers = new int[6];
            for (int i = 0; i < 6; i++)
            {
                SceenOrder[i] = new int[10];
            }

            SceenNumbers[0] = 2;
            SceenOrder[0][0] = 2;
            SceenOrder[0][1] = 4;

            SceenNumbers[1] = 4;
            SceenOrder[1][0] = 3;
            SceenOrder[1][1] = 6;
            SceenOrder[1][2] = 7; 
            SceenOrder[1][3] = 11;

            SceenNumbers[2] = 4;
            SceenOrder[2][0] = 8;
            SceenOrder[2][1] = 9;
            SceenOrder[2][2] = 10;
            SceenOrder[2][3] = 13;

            SceenNumbers[3] = 3;
            SceenOrder[3][0] = 5;
            SceenOrder[3][1] = 12;
            SceenOrder[3][2] = 14;

            SceenNumbers[4] = 4;
            SceenOrder[4][0] = 15;
            SceenOrder[4][1] = 16;
            SceenOrder[4][2] = 17;
            SceenOrder[4][3] = 19;

            SceenNumbers[5] = 1;
            SceenOrder[5][0] = 18;

            wallobjs = new GameObject[TotWalls];

            
        }

        // Behaviour messages
        void Start()
        {
            if (bTutorial == false)
            {
                Initialize();
            }
            
        }

        private void Initialize()
        {
            GameInfo.Score = 0;
            Camera.main.GetComponent<Camera>().backgroundColor = Color.black;
            Level = 0;
            for (int i = 0; i < TotWalls; i++)
            {
                int randwallid = Random.RandomRange(0, SceenNumbers[Level]);
                Vector3 pos = new Vector3(0, 49 + 20 * i, 0);
                wallobjs[i] = GameObject.Instantiate(walls[SceenOrder[Level][randwallid] - 2], pos, Quaternion.identity);
                wallobjs[i].SetActive(false);
                Level++;
                if (Level > 5) Level = 0;
            }

        }

        void Update()
        {
            if (bTutorial)
            {
                return;
            }
            for (int i = 0; i < TotWalls; i++)
            {
                if (playerBall.transform.position.y + 25 > wallobjs[i].transform.position.y)
                {
                    wallobjs[i].SetActive(true);
                }

                if (playerBall.transform.position.y  > wallobjs[i].transform.position.y + 20)
                {
                    wallobjs[i].SetActive(false);
                }


            }
        }


        public void GameOver()
        {
            if (UIManager.instance.gameState == UIManager.GameState.GameOver)
            {
                return;
            }
            UIManager.instance.GameOver();
			//AdsControl.Instance.showAds ();
        }

    }
}
