using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        GameObject.Instantiate(Resources.Load("RedballParticle"), transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
