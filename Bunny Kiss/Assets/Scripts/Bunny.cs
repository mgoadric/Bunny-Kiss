using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum BunnyState
{
    REST, READY, MOVING
}

public class Bunny : Obstacle
{

    public float destx;
    public float desty;

    public float speed;
    public float startTime;

    public BunnyState state = BunnyState.REST;

    public Challenge challenge;
    public Bunny other;
    public GameObject hintfab;

    public List<Vector3> hintLocs;
    public List<GameObject> hintParticles;

    Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        speed = 2.0F;
        m_Animator = gameObject.GetComponent<Animator>();
        hintLocs = new List<Vector3>();
        hintParticles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (state == BunnyState.MOVING)
        {

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered;

            gameObject.transform.position = Vector3.Lerp(Tutorial.S.RelativePos(x, y, -1),
                Tutorial.S.RelativePos(destx, desty, -1), fracJourney);

            if (fracJourney > 1)
            {
                state = BunnyState.REST;
                x = (int)destx;
                y = (int)desty;
                Point(0, gameObject.transform.localEulerAngles.y);
            }
        }
    }

    public void EraseHints()
    {
        foreach (GameObject go in hintParticles)
        {
            Destroy(go);
        }
        hintParticles.Clear();
        hintLocs.Clear();
    }

    public void Point(float zangle, float yangle)
    {
        Vector3 eulerAngles = gameObject.transform.localEulerAngles;
        eulerAngles.z = zangle;
        eulerAngles.y = yangle;
        transform.localRotation = Quaternion.Euler(eulerAngles);
    }

    public void QueueMove(float destx, float desty)
    {
        this.destx = destx;
        this.desty = desty;
        state = BunnyState.READY;
    }

    public bool Move(bool kiss)
    {
        if (state == BunnyState.READY)
        {
            state = BunnyState.MOVING;
            startTime = Time.time;
            m_Animator.SetTrigger("jump");

            if (kiss)
            {
                Point(Math.Sign(desty - y) * 90, Math.Max(0, -Math.Sign(x - destx)) * 180);
                return false;
            }
            else
            {
                Point(Math.Sign(desty - y) * 90, Math.Max(0, Math.Sign(x - destx)) * 180);
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void Kiss(float deltax)
    {
        m_Animator.SetBool("kiss", true);
        QueueMove(x + deltax, y);
        Move(true);
        StartCoroutine("StopKiss");
    }

    IEnumerator StopKiss()
    {
        yield return new WaitForSeconds(3);
        m_Animator.SetTrigger("jump");
        m_Animator.SetBool("kiss", false);
        yield return null;
    }

    public bool Equals(Bunny b)
    {
        return x == b.x && y == b.y;
    }

    public void ResetPosition()
    {
        StartAt(startx, starty);
        state = BunnyState.REST;
        destx = startx;
        desty = starty;
        StopCoroutine("StopKiss");
        m_Animator.SetTrigger("reset");
        m_Animator.SetBool("kiss", false);
        startTime = 0;

    }

    public List<Tuple<int, int>> AllMoves()
    {
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();

        moves.AddRange(LookHere(-1, 0));
        moves.AddRange(LookHere(1, 0));
        moves.AddRange(LookHere(0, -1));
        moves.AddRange(LookHere(0, 1));

        return moves;
    }

    private List<Tuple<int,int>> LookHere(int dx, int dy)
    {
        // Look left
        int obstaclesInWay = 0;
        int distance = 0;
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();

        for (int i = x + dx, j = y + dy; i >= 0 && i < Tutorial.S.XSIZE && j >= 0 && j < Tutorial.S.YSIZE; i += dx, j += dy)
        {
            distance++;
            if (challenge.values[i, j] == 0)
            {
                obstaclesInWay++;
            }
            else if (other.x == i && other.y == j)
            {
                obstaclesInWay++;
            }
            else if (challenge.values[i, j] > 0 && distance - obstaclesInWay == challenge.values[i, j])
            {
                Debug.Log("Can move to " + i + "," + j);
                moves.Add(new Tuple<int, int>(i, j));
                challenge.spaces[i, j].GetComponent<Space>().MakeHint();
            }
        }

        return moves;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked a bunny!");
        if (!challenge.complete && state == BunnyState.REST)
        {

            foreach (Tuple<int, int> move in AllMoves())
            {
                challenge.spaces[move.Item1, move.Item2].GetComponent<Space>().MakeHint();
            }
        }
    }
}
