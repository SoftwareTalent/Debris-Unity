using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage game data
    /// </summary>
    public class GameData
    {
        private static GameData _instance = null;
        public static GameData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameData();
                }

                return _instance;
            }
        }

        // Constructor
        private GameData() { }

        public int GetBirdTypeData { get { return PlayerPrefs.GetInt(Constants.BIRD_TYPE, 1); } }

        public int GetStatusBird2Data { get { return PlayerPrefs.GetInt(Constants.STATUS_BIRD_2, 0); } }
        public int GetStatusBird3Data { get { return PlayerPrefs.GetInt(Constants.STATUS_BIRD_3, 0); } }

        public int GetBestFloorData { get { return PlayerPrefs.GetInt(Constants.BEST_FLOOR, 0); } }
        public int GetGemData { get { return PlayerPrefs.GetInt(Constants.GEM, 0); } }

        public void SaveData(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }
}
