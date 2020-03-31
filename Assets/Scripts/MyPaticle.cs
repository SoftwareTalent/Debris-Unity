using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPaticle : MonoBehaviour {

	// Use this for initialization
    //public float 
    public bool RandomInit = false;
    public bool StarParticle = false;

	void Start () {
        if (RandomInit)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.RandomRange(-2, 2), Random.RandomRange(-2, 2)) * 300);
            int maxrr = 2;
            if (gameObject.name.Contains("Redball")) maxrr = 3;
            float rr = Random.RandomRange(1,maxrr);
            transform.localScale = new Vector3(rr, rr, rr);
        }
        if (StarParticle)
        {
            StartCoroutine(RemoveStar(0.5f));
        }
	}
    IEnumerator RemoveStar(float dt)
    {
        yield return new WaitForSeconds(dt);
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Ground")
        {
 
            Destroy(gameObject);
        }

       
    }
}
