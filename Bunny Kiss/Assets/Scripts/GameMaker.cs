using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaker : MonoBehaviour {

    private int XSIZE = 5;
    private int YSIZE = 5;
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
        Instantiate<GameObject>(bunny1, new Vector3(w / 2, h / 2, 0), Quaternion.identity);
        Instantiate<GameObject>(bunny2, new Vector3(-w / 2, -h / 2, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
