using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public int startx;
    public int starty;

    public int x;
    public int y;

    public GameObject mysprite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartAt(int x, int y)
    {
        this.startx = x;
        this.starty = y;
        this.x = x;
        this.y = y;
        gameObject.transform.position = new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1);
    }
}
