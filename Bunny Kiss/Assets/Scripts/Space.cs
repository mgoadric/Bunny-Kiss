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

    void BunnyCheck(Bunny b, int lookx, int looky)
    {
        if (b.x == lookx && b.y == looky)
        {
            b.MoveTo(x, y);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("" + x + "," + y + ":" + value);
        foreach (Bunny b in challenge.boardBunnies)
        {
            // Look in all four directions for any bunnies that can jump.
            BunnyCheck(b, x + value, y);
            BunnyCheck(b, x - value, y);
            BunnyCheck(b, x, y + value);
            BunnyCheck(b, x, y - value);
        }
    }
}
