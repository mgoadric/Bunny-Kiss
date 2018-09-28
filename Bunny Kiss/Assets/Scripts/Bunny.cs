using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour {

    public int x;
    public int y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo(int x, int y)
    {
        this.x = x;
        this.y = y;

        // SLERP IT LATER, ANIMATE FOR CUTE JUMPING
        gameObject.transform.position = new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1);
    }

    public bool Equals(Bunny b)
    {
        return x == b.x && y == b.y;
    }
}
