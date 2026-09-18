using System;
using UnityEngine;

public class GrapplePointScript : MonoBehaviour
{
    public float xpos;
    public float ypos;

    public float playerXPos;
    public float playerYPos;

    public Vector2 targetPosition;
    public Vector2 directionUnitVector;
    public float xDistance;
    public float yDistance;
    public float totalDistance;

    const float MOVE_SPEED = 50f;
    public float moveSpeed = MOVE_SPEED;
    private Rigidbody2D rb;

    private LineRenderer lr;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;

        lr.SetPosition(0, new Vector3(xpos, ypos, 0));
        lr.SetPosition(1, new Vector3(playerXPos, playerYPos, 0));
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionUnitVector.x * moveSpeed, directionUnitVector.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            moveSpeed = 0f;
        }
    }

    public void FireGrapplePoint(Vector2 tarPos)
    {
        moveSpeed = MOVE_SPEED;

        targetPosition = tarPos;

        xpos = transform.position.x;
        ypos = transform.position.y;

        xDistance = targetPosition.x - xpos;
        yDistance = targetPosition.y - ypos;

        totalDistance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

        directionUnitVector.x = xDistance / totalDistance;
        directionUnitVector.y = yDistance / totalDistance;

        //setting a bunch of variables to use in FixedUpdate
    }

    public void SetPos(float x, float y)
    {
        Vector2 newPos = new Vector2(x, y);

        transform.position = newPos;
    }

    public float GetXPos()
    {
        return xpos;
    }
    public float GetYPos()
    {
        return ypos;
    }
}
