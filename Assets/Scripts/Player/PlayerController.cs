using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Manage character of the player
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private GameObject m_currentBird;
        private ParticleSystem m_birdTailEffect;

        [SerializeField]
        private Text m_bonusGemText;

        private Rigidbody2D m_playerRigid2D;
        private CircleCollider2D mycollider;

        private Vector2 m_velocityMove = Vector3.zero;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float m_frictionOnAir = 1.0f;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float m_forceJump = 13.5f;

        private int m_directionMove;
        private int m_layerMask;

        private bool
            m_jump,
            m_onGround,
            m_die;

        private bool m_blocking;

        // Constructor
        private PlayerController() { }

        // Behaviour messages

        public Sprite[] SpriteBalls;

        public Vector2 velocity;
        void Awake()
        {
            m_playerRigid2D = GetComponent<Rigidbody2D>();
			Application.targetFrameRate = 60;
        }

        // Behaviour messages
        void Start()
        {
            Initialize();
            Debug.Log(PlayerPrefs.GetInt("SelectedBall", 0));
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = SpriteBalls[PlayerPrefs.GetInt("SelectedBall", 0)];

        }

        private void Initialize()
        {
            m_layerMask = LayerMask.GetMask("Enemy");
            m_currentBird = transform.GetChild(0).gameObject;

            mycollider = m_currentBird.GetComponent<CircleCollider2D>();
            string effectTexturePath = null;
            effectTexturePath = "Bird/BirdEffect";
            
            m_birdTailEffect = m_currentBird.GetComponent<ParticleSystem>();
            m_birdTailEffect.GetComponent<ParticleSystemRenderer>().material.mainTexture = Resources.Load<Texture>(effectTexturePath);

            // Move to the left at first
            m_directionMove = -1;
        }

        // Behaviour messages
        bool bStuned = false;
        public void Stun()
        {
            if (bStuned) return;
            bStuned = true;

            Camera.main.GetComponent<CameraFollow>().ActionShake();

            mycollider.isTrigger = false;
            GetComponent<Animator>().enabled = true;

            GetComponent<Animator>().Play("Stun");
            SoundManager.instance.PlaySound(Constants.HAMMER_SOUND);
            StartCoroutine(ResetPlayer(1.5f));
        }

        IEnumerator ResetPlayer(float dt)
        {
            yield return new WaitForSeconds(dt);
            bStuned = false; 
            GetComponent<Animator>().Play("Idle");
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = SpriteBalls[PlayerPrefs.GetInt("SelectedBall", 0)];
            GetComponent<Animator>().enabled = false;
        }
        void Update()
        {
            if (bStuned) return;
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = SpriteBalls[PlayerPrefs.GetInt("SelectedBall", 0)];
            if (!m_die)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_jump = true;
                    m_onGround = false;
                   
                    SoundManager.instance.PlaySound(Constants.BIRD_SOUND);
                }
            }
        }

        // Behaviour messages
        void FixedUpdate()
        {
            if (GameController.instance.bTutorial)
            {
                if (transform.position.y > 150)
                {
                    Application.LoadLevel("Menu");
                    return;
                }
            }
            velocity = GetComponent<Rigidbody2D>().velocity;
            if (!m_die)
            {
                //Move();
                if (m_jump)
                {
                    Jump();
                    m_jump = false;
                }
            }

            if (transform.position.x < -7 || transform.position.x > 7 || transform.position.y < -20)
            {
                GameController.instance.GameOver();
            }
            if (bStuned)
            {
                mycollider.isTrigger = false;
                return;
            }
            if (velocity.y > 0.1f && !m_blocking)
            {
                mycollider.isTrigger = true;
            }
            else
            {
                m_blocking = false;
                mycollider.isTrigger = false;
            }
        }

        private void Jump()
        {
            // Velocity applied on the air
            m_playerRigid2D.velocity = Vector2.up * m_forceJump;
        }

        // Behaviour messages
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Food")
            {
                other.gameObject.GetComponent<Food>().Die();
                if (UIManager.instance.gameState == UIManager.GameState.Playing)
                {
                    if (!GameController.instance.bTutorial)
                    {
                        GameInfo.Score += 2;    
                    }
                    
                }

                SoundManager.instance.PlaySound(Constants.FOOD_SOUND);

            }
            if (other.gameObject.tag == "Ground")
            {
                m_onGround = true;

                GameObject playerparticle = GameObject.Instantiate(Resources.Load("Particles/PlayerParticle") as GameObject, m_currentBird.transform.position, m_currentBird.transform.rotation);
                SoundManager.instance.PlaySound(Constants.DIE_SOUND);
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                GameController.instance.GameOver();
                //return;
            }

            if (other.gameObject.tag == "Wall")
            {
                mycollider.isTrigger = false;
                //Camera.main.GetComponent<CameraFollow>().ActionShake();
            }
            if (other.gameObject.tag == "Enemy")
            {

                other.gameObject.SendMessage("Die");
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                GameObject playerparticle = GameObject.Instantiate(Resources.Load("Particles/PlayerParticle") as GameObject, m_currentBird.transform.position, m_currentBird.transform.rotation);

                SoundManager.instance.PlaySound(Constants.DIE_SOUND);

                GameController.instance.GameOver();
            }
            
        }

        // Behaviour messages
        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                // Turn on bird tail effect
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                m_birdTailEffect.Play();
            }
        }

        // Behaviour messages
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                GameObject playerparticle = GameObject.Instantiate(Resources.Load("Particles/PlayerParticle") as GameObject, m_currentBird.transform.position, m_currentBird.transform.rotation);
                SoundManager.instance.PlaySound(Constants.DIE_SOUND);
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                GameController.instance.GameOver();
                //return;
            }
            if (other.tag == "Enemy")
            {
                //m_die = true;
                //m_playerRigid2D.isKinematic = true;
                //GameController.instance.GameOver();
                other.gameObject.SendMessage("Die");
                GameObject playerparticle = GameObject.Instantiate(Resources.Load("Particles/PlayerParticle") as GameObject, m_currentBird.transform.position, m_currentBird.transform.rotation);
                SoundManager.instance.PlaySound(Constants.DIE_SOUND);
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                GameController.instance.GameOver();
            }
            if (other.tag == "Wall")
            {
                mycollider.isTrigger = false;
               // Camera.main.GetComponent<CameraFollow>().ActionShake();
            }
            if (other.tag == "Food")
            {
                other.GetComponent<Food>().Die();
                if (UIManager.instance.gameState == UIManager.GameState.Playing)
                {
                    if (!GameController.instance.bTutorial)
                    {
                        GameInfo.Score += 2;
                    }
                }
                
                SoundManager.instance.PlaySound(Constants.FOOD_SOUND);
            }

            if (other.tag == "Star")
            {
                other.GetComponent<StarCS>().ShowParticle();
                if (UIManager.instance.gameState == UIManager.GameState.Playing)
                {
                    if (!GameController.instance.bTutorial)
                    {
                        GameInfo.Score += 5;
                        GameInfo.Stars += 1;
                    }
                }
                
                if (other.GetComponent<StarCS>().MovingStar)
                {
                    SoundManager.instance.PlaySound(Constants.ShootinStar);
                }
                else
                {
                    SoundManager.instance.PlaySound(Constants.STAR_SOUND);
                }
            }
            if (other.gameObject.tag == "Block")
            {
                mycollider.isTrigger = false;
                m_blocking = true;
                m_playerRigid2D.AddForce(other.gameObject.transform.up * 30);
                Camera.main.GetComponent<CameraFollow>().ActionShake();
                //SoundManager.instance.PlaySound(Constants.HIT_SOUND);
            }
        }

        // Behaviour messages
        void OnTriggerExit2D(Collider2D other)
        {
            
        }

        private void Redirect()
        {
            // Flip face of bird
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);

            m_directionMove = (m_directionMove == -1 ? 1 : -1);

        }

        public void SavePLayer()
        {
            Collider2D[] allCol = Physics2D.OverlapCircleAll(m_currentBird.transform.position, 2.5f, m_layerMask);
            foreach (Collider2D col in allCol)
            {
                Destroy(col.gameObject);
            }

            StartCoroutine(UIManager.instance.WaitToRePlay());
        }

        public void LiveAgain()
        {
            m_die = false;
            m_playerRigid2D.isKinematic = false;
        }

        public void SetEnable(bool benable)
        {
            bStuned = false;
            if (benable)
            {
                GetComponent<Rigidbody2D>().simulated = true;
                transform.GetChild(0).gameObject.SetActive(true);
                m_die = false;
            }
            else
            {
                GetComponent<Rigidbody2D>().simulated = false;
                transform.GetChild(0).gameObject.SetActive(false);
                m_die = true;
            }
        }
    }
}
