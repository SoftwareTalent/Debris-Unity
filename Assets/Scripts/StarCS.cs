using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCS : MonoBehaviour {

	// Use this for initialization

    Transform[] dirs;
    public Sprite fadeImage;

    public Sprite[] ShootingImages;
    public bool MovingStar = false;
    public float MoveSpeed = 1;

    int simageid = 0;

	void Start () {
        dirs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            dirs[i] = transform.GetChild(i);
        }

        if (MovingStar)
        {
            StartCoroutine(RemoveObject(5));

            string effectTexturePath = null;
            effectTexturePath = "Bird/BirdEffect";
            if (GetComponent<ParticleSystem>())
                GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material.mainTexture = Resources.Load<Texture>(effectTexturePath);
        }
	}

    IEnumerator RemoveObject(float dt)
    {
        yield return new WaitForSeconds(dt);
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
        if (MovingStar)
        {
            transform.Translate(transform.up * MoveSpeed * Time.deltaTime);
            simageid++;
            GetComponent<SpriteRenderer>().sprite = ShootingImages[simageid % 3];
        }
	}

    int pi = 0;
    public void ShowParticle()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = fadeImage;
        if (MovingStar)
        {
            GameObject.Instantiate(Resources.Load("Particles/TriangleParticle"), transform.position, transform.rotation);
        }
        StartCoroutine(SendStar(0.1f));
    }
    IEnumerator SendStar(float dt)
    {
        yield return new WaitForSeconds(dt);
        GameObject star = GameObject.Instantiate(Resources.Load("Particles/StarParticle") as GameObject, dirs[pi % dirs.Length].position, dirs[pi % dirs.Length].rotation);
        star.GetComponent<Rigidbody2D>().AddForce(dirs[pi%dirs.Length].up * 2000);

        pi++;

        if (pi < 12)
        {
            StartCoroutine(SendStar(dt));
        }
        else
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
