using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Challenge : MonoBehaviour {

    public static int XSIZE;
    public static int YSIZE;
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
    private bool ready;


    // Use this for initialization
    void Start() {
    }

    public void SetUp(int x, int y, int[,] values) {

        XSIZE = x;
        YSIZE = y;
        this.values = values;

        // MAKE A RANDOM LEVEL
 //       values = new int[XSIZE, YSIZE];

//        for (int i = 0; i < XSIZE; i++)
 //       {
 //           for (int j = 0; j < YSIZE; j++)
  //          {
   //             values[i, j] = Random.Range(minvalue, maxvalue);
 //           }
 //       }

        obstacles = new List<Obstacle>();
        boardBunnies = new List<Bunny>();

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
                } else if (values[i,j] > 0)
                {
                    GameObject space = Instantiate<GameObject>(spacefab, new Vector3(i - w / 2, j - h / 2, 0), Quaternion.identity);
                    Space script = space.GetComponent<Space>();
                    script.value = values[i, j];
                    script.x = i;
                    script.y = j;
                    script.text.GetComponent<TextMeshPro>().text = "" + script.value;
                    script.challenge = this;
                } else if (values[i,j] == -1)
                {
                    // Put a bunny on the board
                    MakeBunny(i, j, bunnyfab);
                }
            }
        }

        ready = true;
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
    void Update() {
        if (ready && !complete)
        {
            //Debug.Log("1:" + boardBunnies[0].x + "," + boardBunnies[0].y + " 2:" + boardBunnies[1].x + "," + boardBunnies[1].y);
            if (boardBunnies[0].Equals(boardBunnies[1]))
            {
                Debug.Log("Hooray, kiss!");
                Instantiate<GameObject>(heartsfab, new Vector3(boardBunnies[0].transform.position.x, boardBunnies[0].transform.position.y, 0), Quaternion.identity);
                boardBunnies[0].Kiss(0.43f);
                boardBunnies[1].Kiss(-0.43f);
                complete = true;
            }
        }
	}
}
