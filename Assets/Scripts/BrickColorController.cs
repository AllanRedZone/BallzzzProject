using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColorController : MonoBehaviour
{

    public Gradient gradient;
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start ()
	{
	    renderer = GetComponent<SpriteRenderer>();
	    renderer.color = gradient.Evaluate(Random.Range(0, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
