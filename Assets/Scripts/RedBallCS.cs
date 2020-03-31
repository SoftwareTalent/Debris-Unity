using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Debris.CoreSolution
{
    public class RedBallCS : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            string effectTexturePath = null;
            effectTexturePath = "Bird/BirdEffect";
            if (GetComponent<ParticleSystem>())
                GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material.mainTexture = Resources.Load<Texture>(effectTexturePath);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Die()
        {
            GameObject.Instantiate(Resources.Load("Particles/RedballParticle"), transform.position, transform.rotation);
            SoundManager.instance.PlaySound(Constants.REDBALLDIE);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }


        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
                return;
            }

        }
    }

}