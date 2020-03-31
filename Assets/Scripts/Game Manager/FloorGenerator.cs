using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Automatically generates random floors
    /// </summary>
    public class FloorGenerator : MonoBehaviour
    {
        private List<GameObject> m_floorsList;
        private GameObject
            m_leftSide_obj,                 // Make a new object to hold enemies on the left side
            m_rightSide_obj;// Make a new object to hold enemies on the right side

        private Vector3
            m_lastFloorPos,
            m_lastLeftEnemyPos,
            m_lastRightEnemyPos;

        [SerializeField]
        private float m_gapBetweenEnemies = 2.0f;

        private float
            m_floorHeight,
            m_singleEnemyHeight,
            m_groupEnemy2Height,
            m_groupEnemy3Height,
            m_groupEnemy4Height;

        private int
            m_consecutiveAmountLeftEnemies,
            m_consecutiveAmountRightEnemies;

        public static FloorGenerator instance = null;

        public GameObject floorPrefab;
        public GameObject[] enemiesPrefabs;
        public GameObject gemPrefab;
        public GameObject gemEffectPrefab;

        public GameObject floorsObj;
        public Transform firstFloor;

        public int CurrentFloor { get; set; }
        public int HighestFloor { get; set; }

        // Constructor
        private FloorGenerator() { }

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
            m_floorsList = new List<GameObject>();

            m_leftSide_obj = new GameObject("LeftSide");
            m_leftSide_obj.transform.parent = floorsObj.transform;

            m_rightSide_obj = new GameObject("RightSide");
            m_rightSide_obj.transform.parent = floorsObj.transform;

            m_floorHeight = firstFloor.localScale.y;
            m_lastFloorPos = firstFloor.position;

            // Fixed position of first enemy, this is optinal
            m_lastLeftEnemyPos = m_lastRightEnemyPos = new Vector3(0.0f, 22.0f, 0.0f);

            Initialize();
        }

        private void Initialize()
        {
            int temp = 0;
            do
            {
                for (int i = 0; i < enemiesPrefabs.Length; i++)
                {
                    if (enemiesPrefabs[i].name == "GroupEnemy_1_1")
                    {
                        m_singleEnemyHeight = enemiesPrefabs[i].transform.Find("Height").localScale.y;
                        temp++;
                    }
                    else if (enemiesPrefabs[i].name == "GroupEnemy_2")
                    {
                        m_groupEnemy2Height = enemiesPrefabs[i].transform.Find("Height").localScale.y;
                        temp++;
                    }
                    else if (enemiesPrefabs[i].name == "GroupEnemy_3")
                    {
                        m_groupEnemy3Height = enemiesPrefabs[i].transform.Find("Height").localScale.y;
                        temp++;
                    }
                    else if (enemiesPrefabs[i].name == "GroupEnemy_4")
                    {
                        m_groupEnemy4Height = enemiesPrefabs[i].transform.Find("Height").localScale.y;
                        temp++;
                    }
                }
            } while (temp < 4);
        }

        public void GenerateFloor()
        {
            // Create floor background
            GameObject bgFloor = Instantiate(floorPrefab,
                new Vector3(firstFloor.position.x, m_lastFloorPos.y + m_floorHeight - 0.05f, firstFloor.position.z),
                    Quaternion.identity) as GameObject;

            bgFloor.name = "" + HighestFloor;
            m_lastFloorPos = bgFloor.transform.position;

            bgFloor.transform.parent = floorsObj.transform;

            // Create gem
            GameObject gem_clone = Instantiate(gemPrefab,
                new Vector3(firstFloor.position.x + Random.Range(-1.5f, 1.5f), m_lastFloorPos.y - 2.0f, firstFloor.position.z),
                    Quaternion.identity) as GameObject;

            gem_clone.transform.parent = bgFloor.transform;

            CreateEnemiesAndGaps();

            m_floorsList.Add(bgFloor);
        }

        private void CreateEnemiesAndGaps()
        {
            // Enemies on left side
            int i = 0;
            while (i < 10)
            {
                if (Random.value <= 0.5f && m_consecutiveAmountLeftEnemies < 6)
                {
                    GameObject enemy_clone = null;
                    RandomEnemyOnLeftSide(ref enemy_clone, ref m_leftSide_obj);
                }
                else
                {
                    m_lastLeftEnemyPos.y += m_gapBetweenEnemies;
                    m_consecutiveAmountLeftEnemies = 0;
                }
                i++;
            }

            // Enemies on right side
            int j = 0;
            while (j < 10)
            {
                if (Random.value <= 0.5f && m_consecutiveAmountRightEnemies < 6)
                {
                    GameObject enemy_clone = null;
                    RandomEnemyOnRightSide(ref enemy_clone, ref m_rightSide_obj);
                }
                else
                {
                    m_lastRightEnemyPos.y += m_gapBetweenEnemies;
                    m_consecutiveAmountRightEnemies = 0;
                }
                j++;
            }
        }

        private void RandomEnemyOnLeftSide(ref GameObject enemy_clone, ref GameObject leftSide_obj)
        {
            int index = (int)Mathf.Round(Random.Range(0.0f, 5.0f));

            if (index == 0 || index == 1 || index == 2)
                m_consecutiveAmountLeftEnemies++;
            else if (index == 3)
                m_consecutiveAmountLeftEnemies += 2;
            else if (index == 4)
                m_consecutiveAmountLeftEnemies += 3;
            else
                m_consecutiveAmountLeftEnemies += 4;

            if (m_consecutiveAmountLeftEnemies < 6)
            {
                enemy_clone = Instantiate(enemiesPrefabs[index],
                    new Vector3(enemiesPrefabs[index].transform.position.x, m_lastLeftEnemyPos.y, m_lastLeftEnemyPos.z),
                        Quaternion.identity) as GameObject;

                SpriteRenderer[] childs = enemy_clone.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer child in childs)
                {
                    if (child.tag == "Enemy")
                        child.color = GameController.instance.CurrentColor;
                }

                enemy_clone.transform.parent = leftSide_obj.transform;

                float height = AdjustHeightBasedOnIndexes(index);
                m_lastLeftEnemyPos.y = enemy_clone.transform.position.y + height;
            }
        }

        private void RandomEnemyOnRightSide(ref GameObject enemy_clone, ref GameObject rightSide_obj)
        {
            int index = (int)Mathf.Round(Random.Range(0.0f, 5.0f));

            if (index == 0 || index == 1 || index == 2)
                m_consecutiveAmountRightEnemies++;
            else if (index == 3)
                m_consecutiveAmountRightEnemies += 2;
            else if (index == 4)
                m_consecutiveAmountRightEnemies += 3;
            else
                m_consecutiveAmountRightEnemies += 4;

            if (m_consecutiveAmountRightEnemies < 6)
            {
                enemy_clone = Instantiate(enemiesPrefabs[index],
                    new Vector3(enemiesPrefabs[index].transform.position.x, m_lastRightEnemyPos.y, m_lastRightEnemyPos.z),
                        Quaternion.identity) as GameObject;

                // Flip face
                enemy_clone.transform.localScale =
                    new Vector3(enemy_clone.transform.localScale.x * -1.0f, enemy_clone.transform.localScale.y, enemy_clone.transform.localScale.z);

                SpriteRenderer[] childs = enemy_clone.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer child in childs)
                {
                    if (child.tag == "Enemy")
                        child.color = GameController.instance.CurrentColor;
                }

                enemy_clone.transform.parent = rightSide_obj.transform;

                float height = AdjustHeightBasedOnIndexes(index);
                m_lastRightEnemyPos.y = enemy_clone.transform.position.y + height;
            }
        }

        private float AdjustHeightBasedOnIndexes(int index)
        {
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                    return m_singleEnemyHeight;
                case 3:
                    return m_groupEnemy2Height;
                case 4:
                    return m_groupEnemy3Height;
                case 5:
                    return m_groupEnemy4Height;
            }
            return 0.0f;
        }

        public void HideAndShowFloor(float velocity)
        {
            if (CurrentFloor >= 3)
            {
                if (velocity <= 0.0f)
                {
                    m_floorsList[CurrentFloor - 3].SetActive(true);
                }
                else
                {
                    m_floorsList[CurrentFloor - 3].SetActive(false);
                }
            }
        }

        public void CreateGemEffect(Vector3 position, Quaternion quaternion)
        {
            Instantiate(gemEffectPrefab, position, quaternion);
        }
    }
}
