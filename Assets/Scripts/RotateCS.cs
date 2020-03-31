using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCS : MonoBehaviour {

	// Use this for initialization
    public float RotateSpeed = 1;
    public bool bFullRotate = true;
    public bool bClockwise = true;

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * (bClockwise ? 1 : -1) * RotateSpeed);
	}
}
