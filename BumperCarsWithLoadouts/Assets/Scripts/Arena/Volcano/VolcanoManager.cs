using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoManager : MonoBehaviour
{
    public float timeBetweenEruptions = 15f;
    public int rocksPerEruption = 1;
    public int decoRocksPer = 10;
    public int maxEruptions = -1; // use -1 for infinite eruptions
    public GameObject rockGO;
    public GameObject decoRock;
    public AudioClip eruptionSE;

    private float timer;
    private bool active;

	// Use this for initialization
	void Start ()
    {
        timer = 0f;
        active = true;

        //set up the timer
        GameObject.Find("SceneManager").GetComponent<ResetTimer>().enabled = true;
        GameObject.Find("SceneManager").GetComponent<ResetTimer>().timeBtwEvents = timeBetweenEruptions;
        GameObject.Find("SceneManager").GetComponent<ResetTimer>().eventLength = .1f;//slightly off, but it works for now
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (maxEruptions == 0)
            active = false;

        if (!active) return;

        if(timer >= timeBetweenEruptions)
        {
            GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(eruptionSE);
            Erupt();
            timer = 0f;
            maxEruptions--;
        }

        timer += Time.deltaTime;
	}

    private void Erupt()
    {
        for(int i = 0; i < rocksPerEruption; i++)
        {
            Vector2 rand = Random.insideUnitCircle.normalized;
            float randF = Random.Range(51.75f, 67.7f);
            Vector3 randUnit = new Vector3(rand.x * randF, Random.Range(200f, 400f), rand.y * randF);
            Instantiate(rockGO, randUnit, Quaternion.identity);
        }
        for (int i = 0; i < decoRocksPer; i++)
        {
            Vector2 rand = Random.insideUnitCircle.normalized;
            float randF = Random.Range(76f, 100f);
            Vector3 randUnit = new Vector3(rand.x * randF, Random.Range(200f,400f), rand.y * randF);
            Instantiate(decoRock, randUnit, Quaternion.identity);
        }
        StartCoroutine(EruptionEffect());
    }

    IEnumerator EruptionEffect()
    {
        GameObject p = GameObject.Find("Erupt");
        ParticleSystem pSystem = p.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = pSystem.emission;
        for (int i = 0; i < 60; i++)
        {
            em.enabled = true;
            yield return null;
        }
        em.enabled = false;
    }
}
