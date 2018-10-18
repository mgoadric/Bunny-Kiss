using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Challenge : MonoBehaviour {

    public static readonly int XSIZE = 7;
    public static readonly int YSIZE = 7;
    public int minvalue;
    public int maxvalue;
    private int[,] values;
    public GameObject spacefab;
    public GameObject obstaclefab;
    public GameObject bunnyfab;
    public GameObject heartsfab;

    public List<Bunny> boardBunnies;
    public List<Obstacle> obstacles;
    public bool complete;


    // Use this for initialization
    void Start () {

        // MAKE A RANDOM LEVEL
        values = new int[XSIZE, YSIZE];

        for (int i = 0; i < XSIZE; i++)
        {
            for (int j = 0; j < YSIZE; j++)
            {
                values[i, j] = Random.Range(minvalue, maxvalue);
            }
        }

        obstacles = new List<Obstacle>();

        // MAKE THE OBJECTS BASED ON THE LEVEL
        int w = values.GetLength(0);
        int h = values.GetLength(1);
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (values[i,j] == 0)
                {
                    GameObject space = Instantiate<GameObject>(obstaclefab, new Vector3(i - w / 2, j - h / 2, 0), Quaternion.identity);
                    Obstacle ob = space.GetComponent<Obstacle>();
                    ob.x = i;
                    ob.y = j;
                    obstacles.Add(ob);
                } else
                {
                    GameObject space = Instantiate<GameObject>(spacefab, new Vector3(i - w / 2, j - h / 2, 0), Quaternion.identity);
                    Space script = space.GetComponent<Space>();
                    script.value = values[i, j];
                    script.x = i;
                    script.y = j;
                    script.text.GetComponent<TextMeshPro>().text = "" + script.value;
                    script.challenge = this;
                }
               
            }
        }

        // PUT THE BUNNIES ON THE BOARD
        boardBunnies = new List<Bunny>();
        MakeBunny(0, 0, bunnyfab);
        Bunny b2 = MakeBunny(XSIZE - 1, YSIZE - 1, bunnyfab);
        b2.Point(0, 180);

        complete = false;
    }

    Bunny MakeBunny(int x, int y, GameObject bun)
    {
        GameObject bunny = Instantiate<GameObject>(bun);
        Bunny b = bunny.GetComponent<Bunny>();
        b.StartAt(x, y);
        boardBunnies.Add(b);
        return b;
    }

    // Update is called once per frame
    void Update () {
		if (!complete && boardBunnies[0].Equals(boardBunnies[1]))
        {
            Debug.Log("Hooray, kiss!");
            Instantiate<GameObject>(heartsfab, new Vector3(boardBunnies[0].transform.position.x, boardBunnies[0].transform.position.y, 0), Quaternion.identity);
            boardBunnies[0].Kiss();
            boardBunnies[0].QueueMove(boardBunnies[0].x + 0.43f, boardBunnies[0].y);
            boardBunnies[0].Move(true);
            boardBunnies[1].Kiss();
            boardBunnies[1].QueueMove(boardBunnies[1].x - 0.43f, boardBunnies[1].y);
            boardBunnies[1].Move(true);
            complete = true;
        }
	}
}
