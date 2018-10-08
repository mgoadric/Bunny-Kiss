using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyState
{
    REST, READY, MOVING
}

public class Bunny : MonoBehaviour {

    public int x;
    public int y;
    public int destx;
    public int desty;
    public BunnyState state = BunnyState.REST;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QueueMove(int destx, int desty)
    {
        this.destx = destx;
        this.desty = desty;
        state = BunnyState.READY;
    }

    public void Move()
    {
        if (state == BunnyState.READY)
        {
            this.x = destx;
            this.y = desty;
            state = BunnyState.REST;

            // SLERP IT LATER, ANIMATE FOR CUTE JUMPING
            gameObject.transform.position = new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1);
        }
    }

    public bool Equals(Bunny b)
    {
        return x == b.x && y == b.y;
    }
}
