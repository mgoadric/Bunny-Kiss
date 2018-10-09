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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BunnyCheck(Bunny b, Bunny ob, int dx, int dy)
    {
        if (dy == 0)
        {

            int sign = Math.Sign(dx);
            for (int j = x + sign; sign * (j - (x + dx)) <= 0; j += sign)
            {
                if (ob.x == j && ob.y == y)
                {
                    dx += sign;
                    break;
                }
            }
        }
        if (dx == 0)
        {

            int sign = Math.Sign(dy);
            for (int j = y + sign; sign * (j - (y + dy)) <= 0; j += sign)
            {
                if (ob.x == x && ob.y == j)
                {
                    dy += sign;
                    break;
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
        Debug.Log("" + x + "," + y + ":" + value);
        for (int i = 0; i < challenge.boardBunnies.Count; i++) 
        {
            Bunny b = challenge.boardBunnies[i];
            Bunny ob = challenge.boardBunnies[1 - i];

            // Look in all four directions for any bunnies that can jump.
            BunnyCheck(b, ob, value, 0);
            BunnyCheck(b, ob, -value, 0);
            BunnyCheck(b, ob, 0, value);
            BunnyCheck(b, ob, 0, -value);
        }

        foreach (Bunny b in challenge.boardBunnies)
        {
            b.Move();
        }
    }
}
