using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedballSpawn : MonoBehaviour {

	// Use this for initialization
    public GameObject particlePref;
    public Transform Spawnpos;

	void Start () {
         StartCoroutine(SpawnRedBall(5));
    }

    IEnumerator SpawnRedBall(float dt)
    {
        yield return new WaitForSeconds(dt);

        GameObject redball = GameObject.Instantiate(particlePref, Spawnpos.position, Spawnpos.rotation);
        redball.transform.localScale = Spawnpos.localScale;
        StartCoroutine(SpawnRedBall(dt));

    }

	// Update is called once per frame
	void Update () {
		
	}
}
