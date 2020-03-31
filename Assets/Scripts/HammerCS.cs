using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Debris.CoreSolution
{
    public class HammerCS : MonoBehaviour
    {

        // Use this for initialization
        int Life = 1000;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("----");
            if (other.gameObject.tag == "Player" && other.GetComponent<Rigidbody2D>() == null)
            {
                if (Life > 0)
                {
                    Life--;
                    other.transform.parent.GetComponent<PlayerController>().Stun();
                    other.transform.parent.GetComponent<Rigidbody2D>().AddForce(-Vector3.up * 100);
                    SoundManager.instance.PlaySound(Constants.HAMMER_SOUND);
                }
                else
                {
                    SoundManager.instance.PlaySound(Constants.HIT_SOUND);
                    //other.transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
                }
            }

            if (other.gameObject.name.Contains("Triangle(Clone)"))
            {
                other.GetComponent<TriangleCS>().Die();
                SoundManager.instance.PlaySound(Constants.TRIANGLEDIE);
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log("----====");
            if (other.gameObject.name.Contains("Triangle(Clone)"))
            {
                other.gameObject.GetComponent<TriangleCS>().Die();
                SoundManager.instance.PlaySound(Constants.TRIANGLEDIE);
            }
        }
    }
}