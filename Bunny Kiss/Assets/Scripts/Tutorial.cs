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
        { "2\n2\n-1,1\n0,-1",
          "2\n3\n2,1,-1\n-1,1,2",
          "2\n3\n1,1,-1\n-1,2,1",
          "3\n3\n-1,2,0\n1,1,1\n0,2,-1",
          "3\n3\n0,1,-1\n1,2,2\n-1,1,2",
          "3\n3\n1,0,-1\n1,1,2\n-1,2,0",
          "3\n3\n-1,2,2\n2,1,0\n-1,1,1",
          "3\n3\n1,2,-1\n1,2,1\n-1,1,0",
          "3\n4\n2,1,3,-1\n1,3,2,0\n-1,2,1,2",
          "3\n4\n3,1,2,-1\n0,1,2,1\n-1,1,0,0",
          "3\n4\n-1,3,1,2\n1,2,3,3\n0,1,2,-1",
          "4\n4\n-1,2,3,2\n3,0,1,0\n3,0,1,1\n2,1,2,-1",
         // "4\n4\n-1,2,1,1\n2,1,2,2\n2,2,3,3\n-1,0,1,3",

        };

    public int currentLevel;
    public int highestLevel;

    public Challenge challenge;
    public GameObject next;
    public GameObject prev;
    public TextMeshProUGUI moves;
    public TextMeshProUGUI level;
    private int XOFFSET = -3;
    private int YOFFSET = 0;

    public int XSIZE;
    public int YSIZE;

    public static Tutorial S;

    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        currentLevel = 0;
        highestLevel = -1;
        MakeChallenge();

    }

	// Update is called once per frame
	void Update () {
        if (challenge)
        {
            moves.text = "" + challenge.moves;
            if (currentLevel <= highestLevel || challenge.complete)
            {
                next.GetComponent<Button>().interactable = true;
                if (currentLevel > highestLevel)
                {
                    highestLevel = currentLevel;
                }
            } 
            else
            {
                next.GetComponent<Button>().interactable = false;
            }
        }
        if (currentLevel > 0)
        {
            prev.GetComponent<Button>().interactable = true;
        }
        else
        {
            prev.GetComponent<Button>().interactable = false;
        }
	}

    public Vector3 RelativePos(float x, float y, float z)
    {
        return new Vector3((x - XSIZE / 2) + XOFFSET, (y - YSIZE / 2) + YOFFSET, z);
    }

    public void ResetChallenge()
    {
        Debug.Log("Reset clicked");
        if (challenge)
        {
            Debug.Log("Resetting");
            challenge.ResetChallenge();
        }
    }

    public void NextChallenge()
    {
        if (challenge && (currentLevel <= highestLevel || challenge.complete))
        {
            currentLevel++;

            level.text = "" + (currentLevel + 1);
            Destroy(challenge.gameObject);
            MakeChallenge();
        }
    }

    public void PreviousChallenge()
    {
        if (challenge)
        {
            currentLevel--;
            level.text = "" + (currentLevel + 1);
            Destroy(challenge.gameObject);
            MakeChallenge();
        }

    }

    void MakeChallenge()
    {
        level.text = "" + (currentLevel + 1);

        StreamReader reader = new StreamReader(GenerateStreamFromString(levels[currentLevel]));
        char[] delim = { ',' };
        XSIZE = int.Parse(reader.ReadLine());
        YSIZE = int.Parse(reader.ReadLine());
        int[,] board = new int[XSIZE, YSIZE];
        for (int i = 0; i < XSIZE; i++)
        {
            string where = reader.ReadLine();
            string[] poss = where.Split(delim);
            for (int j = 0; j < YSIZE; j++)
            {
                board[i, j] = int.Parse(poss[j]);
            }
        }
        reader.Close();

        GameObject go = Instantiate(challengefab, new Vector3(-4, 0, 0), Quaternion.identity);
        challenge = go.GetComponent<Challenge>();
        challenge.SetUp(XSIZE, YSIZE, board);
        next.GetComponent<Button>().interactable = false;
    }

    public static MemoryStream GenerateStreamFromString(string value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }


}
