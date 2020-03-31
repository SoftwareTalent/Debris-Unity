using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<Rigidbody2D>() == null)
        {
            other.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
