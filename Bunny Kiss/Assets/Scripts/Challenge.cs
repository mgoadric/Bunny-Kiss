using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Challenge : MonoBehaviour
{

    public int moves;
    public int minvalue;
    public int maxvalue;
    public int[,] values;
    public GameObject[,] spaces;
    public GameObject spacefab;
    public GameObject obstaclefab;
    public GameObject bunnyfab;
    public GameObject heartsfab;
    public Sprite burrowSprite;

    public List<Bunny> boardBunnies;
    public List<Obstacle> obstacles;
    public bool complete;
    private bool ready;


    // Use this for initialization
    void Start()
    {
    }

    public void SetUp(int x, int y, int[,] values)
    {

        this.values = values;
        spaces = new GameObject[x, y];

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
                if (values[i, j] == 0)
                {
                    GameObject space = Instantiate<GameObject>(obstaclefab, Tutorial.S.RelativePos(i, j, 0), Quaternion.identity);
                    space.transform.parent = this.transform;
                    Obstacle ob = space.GetComponent<Obstacle>();
                    ob.x = i;
                    ob.y = j;
                    obstacles.Add(ob);
                }
                else if (values[i, j] > 0)
                {
                    GameObject space = Instantiate<GameObject>(spacefab, Tutorial.S.RelativePos(i, j, 0), Quaternion.identity);
                    spaces[i, j] = space;
                    space.transform.parent = this.transform;
                    Space script = space.GetComponent<Space>();
                    script.value = values[i, j];
                    script.x = i;
                    script.y = j;
                    script.text.GetComponent<TextMeshPro>().text = "" + script.value;
                    script.challenge = this;
                }
                else if (values[i, j] == -1)
                {
                    // Put a bunny on the board
                    GameObject space = Instantiate<GameObject>(obstaclefab, Tutorial.S.RelativePos(i, j, 0), Quaternion.identity);
                    space.transform.parent = this.transform;
                    SpriteRenderer sr = space.GetComponent<Obstacle>().mysprite.GetComponent<SpriteRenderer>();
                    sr.sprite = burrowSprite;
                    MakeBunny(i, j, bunnyfab);
                }
            }
        }

        for (int i = 0; i < boardBunnies.Count; i++)
        {
            boardBunnies[i].other = boardBunnies[1 - i];
        }

        ready = true;
        complete = false;
    }

    Bunny MakeBunny(int x, int y, GameObject bun)
    {
        GameObject bunny = Instantiate<GameObject>(bun);
        bunny.transform.parent = this.transform;

        Bunny b = bunny.GetComponent<Bunny>();
        b.challenge = this;
        b.StartAt(x, y);
        boardBunnies.Add(b);
        return b;
    }

    public void ResetChallenge()
    {
        foreach (Bunny b in boardBunnies)
        {
            b.ResetPosition();
        }
        complete = false;
        moves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && !complete)
        {
            //Debug.Log("1:" + boardBunnies[0].x + "," + boardBunnies[0].y + " 2:" + boardBunnies[1].x + "," + boardBunnies[1].y);
            if (boardBunnies[0].Equals(boardBunnies[1]))
            {
                Debug.Log("Hooray, kiss!");
                GameObject hearts = Instantiate<GameObject>(heartsfab, Tutorial.S.RelativePos(boardBunnies[0].x, boardBunnies[0].y, 0), Quaternion.identity);
                hearts.transform.parent = this.transform;

                boardBunnies[0].Kiss(0.43f);
                boardBunnies[1].Kiss(-0.43f);
                complete = true;
            }
        }
    }
}
