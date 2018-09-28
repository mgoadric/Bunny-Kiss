using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour {

    public Challenge challenge;
    public int value;
    public int x;
    public int y;
    public GameObject text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("" + x + "," + y + ":" + value);
    }
}
