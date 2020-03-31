using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Debris.CoreSolution
{
    public class Food : MonoBehaviour
    {

        // Use this for initialization

        public Sprite fadeImage;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Die()
        {
            GetComponent<CircleCollider2D>().enabled = false;
            if (GameController.instance.bTutorial)
            {
                StartCoroutine(RemoveMe(0.5f));
                return;
            }
            StartCoroutine(RemoveMe(0.5f));
            GetComponent<SpriteRenderer>().sprite = fadeImage;
            GameObject.Instantiate(Resources.Load("Particles/FoodParticle"), transform.position, transform.rotation);
        }

        IEnumerator RemoveMe(float dt)
        {
            yield return new WaitForSeconds(dt);
            Destroy(gameObject);
        }
    }

}