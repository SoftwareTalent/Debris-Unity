using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage sounds in game
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        
        public AudioSource m_buttonSound;

        public AudioSource m_birdSound;

        public AudioSource m_birdDieSound;

        public AudioSource m_hitSound;
        public AudioSource m_hammerSound;
        public AudioSource m_starSound;
        public AudioSource m_foodSound;
        public AudioSource m_backbuttonSound;

        public AudioSource m_TriangleDie;
        public AudioSource m_RedballDie;
        public AudioSource m_ShootingStarSound;
        public AudioSource m_BallSelectSound;
        public static SoundManager instance = null;
        public AudioSource m_backmusic;
        // Constructor
        private SoundManager() { }

        // Behaviour messages
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

        }

        void Start()
        {
            if (GameInfo.bSoundOn == false)
            {
                m_backmusic.mute = true;
            }
            else
            {
                m_backmusic.mute = false;
            }
        }

        public void PlaySound(string s)
        {
            //if (GameInfo.bSoundOn == false) return;

            switch (s)
            {
                case Constants.BUTTON_SOUND:
                    m_buttonSound.Play();
                    break;
                case Constants.BIRD_SOUND:
                    m_birdSound.Play();
                    break;
                case Constants.DIE_SOUND:
                    m_birdDieSound.Play();
                    break;
                case Constants.HIT_SOUND:
                    m_hitSound.Play();
                    break;
                case Constants.FOOD_SOUND:
                    m_foodSound.Play();
                    break;
                case Constants.STAR_SOUND:
                    m_starSound.Play();
                    break;
                case Constants.HAMMER_SOUND:
                    m_hammerSound.Play();
                    break;
                case Constants.BackMenuSound:
                    m_backbuttonSound.Play();
                    break;
                case Constants.ShootinStar:
                    m_ShootingStarSound.Play();
                    break;
                case Constants.REDBALLDIE:
                    m_RedballDie.Play();
                    break;
                case Constants.TRIANGLEDIE:
                    m_TriangleDie.Play();
                    break;
                case Constants.BALLSELECT:
                    m_BallSelectSound.Play();
                    break;
            }
        }
    }
}
