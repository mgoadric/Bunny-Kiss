using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum BunnyState
{
    REST, READY, MOVING
}

public class Bunny : Obstacle {

    public int destx;
    public int desty;

    public float speed;
    public float startTime;

    public BunnyState state = BunnyState.REST;

    Animator m_Animator;

    // Use this for initialization
    void Start () {
        speed = 2.0F;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (state == BunnyState.MOVING)
        {

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered;

            gameObject.transform.position = Vector3.Lerp(new Vector3(x - Challenge.XSIZE / 2, y - Challenge.YSIZE / 2, -1),
                new Vector3(destx - Challenge.XSIZE / 2, desty - Challenge.YSIZE / 2, -1), fracJourney);

            if (fracJourney > 1)
            {
                state = BunnyState.REST;
                x = destx;
                y = desty;
                Point(0, gameObject.transform.localEulerAngles.y);
            }
        }
		
	}

    public void Point(float zangle, float yangle)
    {
        Vector3 eulerAngles = gameObject.transform.localEulerAngles;
        eulerAngles.z = zangle;
        eulerAngles.y = yangle;
        transform.localRotation = Quaternion.Euler(eulerAngles);
    }

    public void QueueMove(int destx, int desty)
    {
        this.destx = destx;
        this.desty = desty;
        state = BunnyState.READY;
    }

    public void Move()
    {
        if (state == BunnyState.READY)
        {
            state = BunnyState.MOVING;
            startTime = Time.time;
            m_Animator.SetTrigger("jump");

            Point(Math.Sign(desty - y) * 90, Math.Max(0, Math.Sign(x - destx)) * 180);
        }
    }

    public bool Equals(Bunny b)
    {
        return x == b.x && y == b.y;
    }
}
