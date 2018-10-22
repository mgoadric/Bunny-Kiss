using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Tutorial : MonoBehaviour {


    public GameObject challengefab;

    private string[] levels = new string[]
        { "2\n3\n1,1,-1\n-1,2,1",
          "2\n3\n2,1,-1\n-1,1,2",
          "3\n3\n-1,2,0\n1,1,1\n0,2,-1",
          "3\n3\n0,1,-1\n1,2,2\n-1,1,2",
          "3\n3\n1,0,-1\n1,1,2\n-1,2,0",
          "3\n3\n-1,2,2\n2,1,0\n-1,1,1",
          "3\n3\n1,2,-1\n1,2,1\n-1,1,0",
          "3\n4\n2,1,3,-1\n1,3,2,0\n-1,2,1,2",
          //"4\n4\n-1,2,3,3\n3,0,1,0\n3,0,1,1\n2,1,2,-1"
        };

    public int currentLevel;
    public GameObject challenge;

    public static Tutorial S;

    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        currentLevel = 7;

        MakeChallenge();

    }

	// Update is called once per frame
	void Update () {
		
	}

    public static MemoryStream GenerateStreamFromString(string value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }


    void MakeChallenge()
    {
        StreamReader reader = new StreamReader(GenerateStreamFromString(levels[currentLevel]));
        char[] delim = { ',' };
        int xsize = int.Parse(reader.ReadLine());
        int ysize = int.Parse(reader.ReadLine());
        int[,] board = new int[xsize, ysize];
        for (int i = 0; i < xsize; i++)
        {
            string where = reader.ReadLine();
            string[] poss = where.Split(delim);
            for (int j = 0; j < ysize; j++)
            {
                board[i, j] = int.Parse(poss[j]);
            }
        }
        reader.Close();

        challenge = Instantiate(challengefab);
        Challenge c = challenge.GetComponent<Challenge>();
        c.SetUp(xsize, ysize, board);
    }
}
