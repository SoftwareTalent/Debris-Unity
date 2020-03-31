using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Debris.CoreSolution
{
    public class TriangleCS : MonoBehaviour
    {

        // Use this for initialization
        public enum TriangleType
        {
            RandomInit = 0,
            StaticTotate,
            LinerMove,
            BigCircleRotate,
        }

        public TriangleType trinangleType;

        public float fDelayTime;
        public float AngleRotate;
        public float SpeedRotate;
        bool CanRotate = false;

        public bool bRightMove = true;

        public void StartLinerMove(float tm)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", transform.position + transform.up * 15, "time", tm, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            StartCoroutine(RemoveObject(tm));
        }


        public void StartRotateBigCircle(float angle, float speed, float lifetime)
        {
            CanRotate = true;
            AngleRotate = angle;
            SpeedRotate = speed;
            trinangleType = TriangleType.BigCircleRotate;

            StartCoroutine(RemoveObject(lifetime));
        }

        IEnumerator IEStartRandomInitMove(float dist, float movetm)
        {
            yield return new WaitForSeconds(fDelayTime);
            iTween.MoveTo(gameObject, transform.position + transform.up * dist, movetm);
        }



        void Start()
        {
            if (trinangleType == TriangleType.RandomInit)
            {
                Vector3 dir = new Vector3(0, 0, Random.RandomRange(-125, -45));
                if (bRightMove == false)
                {
                    dir = new Vector3(0, 0, Random.RandomRange(36, 125));
                }
                transform.eulerAngles = dir;
                float dist = Random.RandomRange(2, 4);
                float movetm = Random.RandomRange(2, 4);

                StartCoroutine(IEStartRandomInitMove(dist, movetm));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (trinangleType == TriangleType.BigCircleRotate && CanRotate)
            {
                transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * AngleRotate);
                transform.Translate(transform.up * Time.deltaTime * SpeedRotate, Space.World);

            }
        }
        //IEnumerator Rotate(float rotatetime, bool rotatedir)
        //{
        //    Vector3 nextRotate = new Vector3(0, 0, fInitAngle * (rotatedir == bClockwise ? -1.0f : 1.0f));
        //    iTween.RotateTo(gameObject, iTween.Hash("rotation", nextRotate, "time", rotatetime, "eastype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
        //    yield return new WaitForSeconds(rotatetime / 2);
        //    StartCoroutine(Rotate(rotatetime, !rotatedir));
        //}
        public void Die()
    {
        GameObject.Instantiate(Resources.Load("Particles/TriangleParticle"), transform.position, transform.rotation);
        SoundManager.instance.PlaySound(Constants.TRIANGLEDIE);
        Destroy(gameObject);
    }

        IEnumerator RemoveObject(float dt)
        {
            yield return new WaitForSeconds(dt);
            Destroy(gameObject);
        }
    }
}