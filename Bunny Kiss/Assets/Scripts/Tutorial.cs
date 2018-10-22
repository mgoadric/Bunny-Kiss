using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {


    public GameObject challengefab;

    private readonly string[] levels = new string[]
        { "2\n3\n1,1,-1\n-1,2,1",
          "2\n3\n2,1,-1\n-1,1,2",
          "3\n3\n-1,2,0\n1,1,1\n0,2,-1",
          "3\n3\n0,1,-1\n1,2,2\n-1,1,2",
          "3\n3\n1,0,-1\n1,1,2\n-1,2,0",
          "3\n3\n-1,2,2\n2,1,0\n-1,1,1",
          "3\n3\n1,2,-1\n1,2,1\n-1,1,0",
          "3\n4\n2,1,3,-1\n1,3,2,0\n-1,2,1,2",
          "4\n4\n-1,2,3,2\n3,0,1,0\n3,0,1,1\n2,1,2,-1"
        };

    public int currentLevel;
    public Challenge challenge;
    public GameObject next;
    public TextMeshProUGUI moves;

    public static Tutorial S;

    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        currentLevel = 8;

        MakeChallenge();

    }

	// Update is called once per frame
	void Update () {
        if (challenge)
        {
            moves.text = "Moves: " + challenge.moves;
            if (challenge.complete)
            {
                next.SetActive(true); 
            } 
            else
            {
                next.SetActive(false);
            }
        }
	}

    public void ResetChallenge()
    {
        if (challenge)
        {
            challenge.ResetChallenge();
        }
    }

    public void NextChallenge()
    {
        if (challenge && challenge.complete)
        {
            currentLevel++;
            Destroy(challenge.gameObject);
            MakeChallenge();
        }
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

        GameObject go = Instantiate(challengefab);
        challenge = go.GetComponent<Challenge>();
        challenge.SetUp(xsize, ysize, board);
        next.SetActive(false);
    }

    public static MemoryStream GenerateStreamFromString(string value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }


}
