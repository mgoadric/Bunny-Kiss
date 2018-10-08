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

    void BunnyCheck(Bunny b, Bunny ob, int dx, int dy)
    {
        if (dy == 0)
        {
            if (dx > 0)
            {
                for (int j = x + 1; j <= x + dx; j++)
                {
                    if (ob.x == j && ob.y == y)
                    {
                        dx++;
                        break;
                    }
                }
            }
            else if (dx < 0)
            {
                for (int j = x - 1; j >= x + dx; j--)
                {
                    if (ob.x == j && ob.y == y)
                    {
                        dx--;
                        break;
                    }
                }
            }
        }
        if (dx == 0)
        {
            if (dy > 0)
            {
                for (int j = y + 1; j <= y + dy; j++)
                {
                    if (ob.x == x && ob.y == j)
                    {
                        dy++;
                        break;
                    }
                }
            }
            else if (dy < 0)
            {
                for (int j = y - 1; j >= y + dy; j--)
                {
                    if (ob.x == x && ob.y == j)
                    {
                        dy--;
                        break;
                    }
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
