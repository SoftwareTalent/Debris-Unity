using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCS : MonoBehaviour {

	// Use this for initialization

    public enum BarType
    {
        StaticBar = 0,
        RotateBar,
        MoveBar,
    };

    public BarType barType;


    public bool bRepeat = false;

    public float fDelayTime = 0;

    public float fMoveTime = 1;
    public float fMoveDistance = 1;
    public bool bFirstMoveDir = true;

    float fInitAngle = 0;
    public float fRotateTime = 1;
    public bool bClockwise = true;
	void Start () {

        fInitAngle = transform.eulerAngles.z;
        //Debug.Log(fInitAngle);
        StartCoroutine(StartBarAnimation(fDelayTime));
	}

    IEnumerator StartBarAnimation(float delaytm)
    {
        yield return new WaitForSeconds(delaytm);

        if (barType == BarType.RotateBar)
        {
            StartCoroutine(Rotate(fRotateTime, true));
        }
        else if (barType == BarType.MoveBar)
        {
            StartCoroutine(Move(fMoveTime, fMoveDistance, true));
        }
    }

    IEnumerator Rotate(float rotatetime, bool rotatedir)
    {
        Vector3 nextRotate = new Vector3(0, 0, fInitAngle * (rotatedir == bClockwise ? -1.0f : 1.0f));
        iTween.RotateTo(gameObject, iTween.Hash("rotation", nextRotate, "time", rotatetime, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
        yield return new WaitForSeconds(rotatetime);
        StartCoroutine(Rotate(rotatetime, !rotatedir));
    }
	
    IEnumerator Move( float movetime, float movedistance, bool forward)
    {

        Vector3 nextPos = transform.position + transform.right * movedistance * (forward == bFirstMoveDir ? 1 : -1);

        iTween.MoveTo(gameObject, iTween.Hash("position", nextPos, "time", movetime, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));

        yield return new WaitForSeconds(movetime);

        StartCoroutine(Move(movetime, movedistance, !forward));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
