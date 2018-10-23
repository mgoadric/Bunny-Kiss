using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum BunnyState
{
    REST, READY, MOVING
}

public class Bunny : Obstacle {

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
    void Start () {
        speed = 2.0F;
        m_Animator = gameObject.GetComponent<Animator>();
        hintLocs = new List<Vector3>();
        hintParticles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {

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

    public void DrawHints()
    {
        if (state == BunnyState.REST)
        {
            foreach (Vector3 endpoint in hintLocs)
            {
                GameObject go = Instantiate(hintfab, endpoint, Quaternion.identity);
                hintParticles.Add(go);
            }
        }
    }

    public void EraseHints()
    {
        foreach (GameObject go in hintParticles)
        {
            Destroy(go);
        }
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

    private void OnMouseDown()
    {
        Debug.Log("Clicked a bunny!");
        if (!challenge.complete)
        {
            int obstaclesInWay = 0;
            int distance = 0;
            for (int i = x - 1; i >= 0; i--)
            {
                distance++;
                if (challenge.values[i, y] == 0)
                {
                    obstaclesInWay++;
                } else if (other.x == i && other.y == y) {
                    obstaclesInWay++;
                } else if (challenge.values[i, y] > 0 && distance - obstaclesInWay == challenge.values[i, y])
                {
                    Debug.Log("Can move to " + i + "," + y);
                    hintLocs.Add(Tutorial.S.RelativePos(i, y, -2));
                }
            }
            DrawHints();
        }
    }

    private void OnMouseUp()
    {
        hintLocs.Clear();
        EraseHints();
    }
}
