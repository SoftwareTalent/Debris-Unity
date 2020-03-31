using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Debris.CoreSolution
{
    public class TriangleSpawn : MonoBehaviour
    {

        // Use this for initialization
        public GameObject particlePref;
        public Transform Spawnpos;

        public TriangleCS.TriangleType spawntrinangleType;
        public float fSpawnTime = 5;
        public float fLifeTime = 5;

        [Header("Static Move")]
        public float fMoveTime = 5;

        [Header("BigCircleRotate")]
        public float AngleRotate;
        public float SpeedRotate;


        void Start()
        {
            StartCoroutine(SpawnTriangle(fSpawnTime));
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator SpawnTriangle(float dt)
        {
            yield return new WaitForSeconds(dt);

            GameObject triangle = GameObject.Instantiate(particlePref, Spawnpos.position, Spawnpos.rotation);
            triangle.transform.localScale = Spawnpos.localScale;
            triangle.GetComponent<TriangleCS>().trinangleType = spawntrinangleType;

            if (spawntrinangleType == TriangleCS.TriangleType.LinerMove)
            {
                triangle.GetComponent<TriangleCS>().StartLinerMove(fMoveTime);
            }
            else if (spawntrinangleType == TriangleCS.TriangleType.BigCircleRotate)
            {
                triangle.GetComponent<TriangleCS>().StartRotateBigCircle(AngleRotate, SpeedRotate, fLifeTime);
            }
            //iTween.MoveTo(triangle, triangle.transform.position + triangle.transform.up * 15, dt * 2);

            StartCoroutine(SpawnTriangle(dt));

        }
    }
}