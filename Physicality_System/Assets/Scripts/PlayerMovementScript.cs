using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float xpos;
    public float ypos;

    public Vector2 mousePositionOnClick;
    public int grapplePointToFireNext = 0;

    public GameObject[] GrapplePoints;
    const int GRAPLLE_POINT_NUM = 4;

    const float PLAYER_SPEED = 25;
    const float PROJECTILE_SPREAD = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GrapplePoints = new GameObject[GRAPLLE_POINT_NUM];

        for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
        {
            GrapplePoints[i] = Instantiate(Resources.Load<GameObject>("GrapplePoint"), Vector2.zero, Quaternion.identity);
            GrapplePoints[i].GetComponent<GrapplePointScript>().targetPosition = new Vector2(i, i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePostion();

        CheckMouse();
    }

    void UpdatePostion()
    {
        float newXPos = 0;
        float newYPos = 0;

        //averaging position of all grapple points
        for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
        {
            newXPos += GrapplePoints[i].GetComponent<GrapplePointScript>().GetXPos();
            newYPos += GrapplePoints[i].GetComponent<GrapplePointScript>().GetYPos();
        }

        newXPos /= GRAPLLE_POINT_NUM;
        newYPos /= GRAPLLE_POINT_NUM;

        Vector3 newPos = new Vector2(newXPos, newYPos);

        //cool MoveTowards function! for "animating" in straight lines
        transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * PLAYER_SPEED); //https://discussions.unity.com/t/2d-mouse-point-click-movement-system-quick-tutorial/523253

        xpos = transform.position.x;
        ypos = transform.position.y;

        for(int i = 0; i < GRAPLLE_POINT_NUM; i++)
        {
            GrapplePoints[i].GetComponent<GrapplePointScript>().playerXPos = xpos;
            GrapplePoints[i].GetComponent<GrapplePointScript>().playerYPos = ypos;
        }
    }

    void CheckMouse()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePositionOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); //https://discussions.unity.com/t/2d-mouse-point-click-movement-system-quick-tutorial/523253

            FireGrapplePoint(mousePositionOnClick);

            UpdateNextGrapplePoint();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mousePositionOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //projectile spread attack! (math on whiteboard)

            //get next three projectiles
            GameObject projectile1 = GrapplePoints[grapplePointToFireNext];
            UpdateNextGrapplePoint();
            GameObject projectile2 = GrapplePoints[grapplePointToFireNext];
            UpdateNextGrapplePoint();
            GameObject projectile3 = GrapplePoints[grapplePointToFireNext];
            UpdateNextGrapplePoint();

            //setup math
            float xDistance = mousePositionOnClick.x - xpos;
            float yDistance = mousePositionOnClick.y - ypos;

            Vector2 projectile1Target = mousePositionOnClick;
            Vector2 projectile2Target = new Vector2((-yDistance / PROJECTILE_SPREAD) + mousePositionOnClick.x, ( xDistance / PROJECTILE_SPREAD) + mousePositionOnClick.y);
            Vector2 projectile3Target = new Vector2(( yDistance / PROJECTILE_SPREAD) + mousePositionOnClick.x, (-xDistance / PROJECTILE_SPREAD) + mousePositionOnClick.y);

            //fire !!!
            FireGrapplePoint(projectile1, projectile1Target);
            FireGrapplePoint(projectile2, projectile2Target);
            FireGrapplePoint(projectile3, projectile3Target);
            




            //for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
            //{
            //    GrapplePoints[i].GetComponent<GrapplePointScript>().SetPos(xpos, ypos);
            //    GrapplePoints[i].GetComponent<GrapplePointScript>().FireGrapplePoint(mousePositionOnClick);

            //}
        }
    }

    void UpdateNextGrapplePoint()
    {
        GrapplePoints[grapplePointToFireNext].GetComponent<SpriteRenderer>().color = Color.cyan; //default color

        grapplePointToFireNext++;
        if (grapplePointToFireNext >= GRAPLLE_POINT_NUM)
        {
            grapplePointToFireNext = 0;
        }

        GrapplePoints[grapplePointToFireNext].GetComponent<SpriteRenderer>().color = Color.darkCyan; //active color
    }

    void FireGrapplePoint(Vector2 tarPos)
    {
        GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().SetPos(xpos, ypos);
        GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().FireGrapplePoint(tarPos);
    }

    void FireGrapplePoint(GameObject grapplePoint, Vector2 tarPos)
    {
        grapplePoint.GetComponent<GrapplePointScript>().SetPos(xpos, ypos);
        grapplePoint.GetComponent<GrapplePointScript>().FireGrapplePoint(tarPos);
    }
}
