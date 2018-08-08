using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeManager : MonoBehaviour
{
    public float lifetime;
    private float lifetimeSeconds;
	// Use this for initialization
	void Start ()
	{
	    lifetimeSeconds = lifetime;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    lifetimeSeconds -= Time.deltaTime;
	    if (lifetimeSeconds <= 0)
	    {
            gameObject.SetActive(false);
	        //Destroy(gameObject); ///?????
	    }
	}
}
