using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Control the camera to follow player
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        private float m_OffsetX;
        private float m_OffsetZ;
        private Vector3 m_CurrentVelocity;

        public Transform target;
        public float smoothTime = 0.45f;

        public bool Shaking;
        private float ShakeDecay;
        private float ShakeIntensity;
        private Vector3 OriginalPos;
        private Quaternion OriginalRot;

        // Constructor
        private CameraFollow() { }

        // Behaviour messages
        private void Start()
        {
            Shaking = false; 
            Initialize();
        }

        private void Initialize()
        {
            m_OffsetX = transform.position.x;
            m_OffsetZ = transform.position.z;
            transform.parent = null;
        }

        public void ActionShake()
        {
            DoShake();
        }

        // Behaviour messages

        void Update()
        {
            if (ShakeIntensity > 0)
            {
                transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
                transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                          OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                          OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                          OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

                ShakeIntensity -= ShakeDecay;
            }
            else if (Shaking)
            {
                Shaking = false;
            }

        }
        void LateUpdate()
        {
            if (Shaking == false)
            {
                if (target.GetComponent<PlayerController>().velocity.y > 0.1f)
                {
                    Vector3 targetPosition = new Vector3(m_OffsetX, target.position.y, m_OffsetZ);
                    if (targetPosition.y > transform.position.y)
                        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_CurrentVelocity, smoothTime);
                }
                if (UIManager.instance.gameState == UIManager.GameState.Playing)
                {
                    transform.Translate(transform.up * Time.deltaTime * 0.2f);
                }
            }
        }
        public void DoShake()
        {
            OriginalPos = transform.position;
            OriginalRot = transform.rotation;

            ShakeIntensity = 0.2f;
            ShakeDecay = 0.02f;
            Shaking = true;
        }   
    }
}
