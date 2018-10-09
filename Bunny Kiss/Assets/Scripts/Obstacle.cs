using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {

    public int x;
    public int y;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartAt(int x, int y)
    {
        this.x = x;
        this.y = y;
        gameObject.transform.position = new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1);
    }
}
