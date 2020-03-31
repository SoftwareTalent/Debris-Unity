using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("-=-=");
    }

    // Behaviour messages
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("-=-=111");
       
    }
}
