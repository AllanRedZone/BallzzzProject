using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLine : MonoBehaviour {
    private LineRenderer lR;
    public List<Vector2> positions;
    private Renderer rend;
	// Use this for initialization
	void Start () {
        lR = GetComponent<LineRenderer>();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //var line = gameObject.GetComponent<LineRenderer>();
        //var distance = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
        //line.materials[0].mainTextureScale = new Vector3(distance, 1, 1);
        //lR.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
        //lR.material.SetTextureScale("_MainTex", new Vector2(lR.GetPosition(1).magnitude, 1f));
        //line.SetPosition(0, newPosition);
	}
}
