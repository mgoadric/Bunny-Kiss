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

    public float speed = 3.0f;
    public float startTime;

    public BunnyState state = BunnyState.REST;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (state == BunnyState.MOVING)
        {

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered;

            gameObject.transform.position = Vector3.Lerp(new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1),
                new Vector3(destx - Challenge.XSIZE / 2, desty - Challenge.YSIZE / 2, -1), fracJourney);

            if (fracJourney > 1)
            {
                state = BunnyState.REST;
                x = destx;
                y = desty;
            }
        }
		
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
            state = BunnyState.MOVING;
            startTime = Time.time;
        }
    }

    public void StartAt(int x, int y)
    {
        this.x = x;
        this.y = y;
        gameObject.transform.position = new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1);
    }

    public bool Equals(Bunny b)
    {
        return x == b.x && y == b.y;
    }
}
