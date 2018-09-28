using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaker : MonoBehaviour {

    public static readonly int XSIZE = 5;
    public static readonly int YSIZE = 5;
    private int[,] values;
    public GameObject spacefab;
    public GameObject bunny1;
    public GameObject bunny2;

	// Use this for initialization
	void Start () {

        // MAKE A RANDOM LEVEL
        values = new int[XSIZE, YSIZE];

        for (int i = 0; i < XSIZE; i++)
        {
            for (int j = 0; j < YSIZE; j++)
            {
                values[i, j] = Random.Range(0, 5);
            }
        }

        // MAKE THE OBJECTS BASED ON THE LEVEL
        int w = values.GetLength(0);
        int h = values.GetLength(1);
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                GameObject space = Instantiate<GameObject>(spacefab, new Vector3(i - w / 2, j - h / 2, 0), Quaternion.identity);
                Space script = space.GetComponent<Space>();
                script.value = values[i, j];
                script.text.GetComponent<TextMeshPro>().text = "" + script.value;
            }
        }

        // PUT THE BUNNIES ON THE BOARD
        MakeBunny(0, 0, bunny1);
        MakeBunny(XSIZE - 1, YSIZE - 1, bunny2);
    }

    void MakeBunny(int x, int y, GameObject bun)
    {
        GameObject bun1 = Instantiate<GameObject>(bun);
        Bunny b = bun1.GetComponent<Bunny>();
        b.MoveTo(x, y);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
