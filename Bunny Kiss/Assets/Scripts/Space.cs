using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Space : MonoBehaviour {

    public Challenge challenge;
    public int value;
    public int x;
    public int y;
    public GameObject text;
    public GameObject field;
    public Sprite background;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BunnyCheck(Bunny b, List<Obstacle> obs, int dx, int dy)
    {

        int signx = Math.Sign(dx);
        int signy = Math.Sign(dy);
        for (int j = x + signx, k = y + signy; 
            signx * (j - (x + dx)) <= 0 && signy * (k - (y + dy)) <= 0; 
            j += signx, k += signy)
        {
            foreach (Obstacle ob in obs)
            {
                if (ob.x == j && ob.y == k)
                {
                    dy += signy;
                    dx += signx;
                }
            }
        }

        if (b.x == x + dx && b.y == y + dy)
        {
            b.QueueMove(x, y);
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("" + x + "," + y + ":" + value);

        // No clicks allowed when the challenge is completed.
        if (challenge.complete)
        {
            return;
        }

        for (int i = 0; i < challenge.boardBunnies.Count; i++) 
        {

            Bunny b = challenge.boardBunnies[i];

            // No clicking on spaces occupied by a bunny.
            if (b.x == x && b.y == y)
            {
                return;
            }

            // When bunny is resting, it can move.
            if (b.state == BunnyState.REST)
            {
                Bunny other = b.other;

                // Look in all four directions for any bunnies that can jump.
                List<Obstacle> obs = new List<Obstacle>();
                obs.Add(other);
                obs.AddRange(challenge.obstacles);

                BunnyCheck(b, obs, value, 0);
                BunnyCheck(b, obs, -value, 0);
                BunnyCheck(b, obs, 0, value);
                BunnyCheck(b, obs, 0, -value);
            }
        }

        bool anymove = false;
        foreach (Bunny b in challenge.boardBunnies)
        {
            anymove |= b.Move(false);
        }
        if (anymove)
        {
            challenge.moves++;
        }
    }
}
