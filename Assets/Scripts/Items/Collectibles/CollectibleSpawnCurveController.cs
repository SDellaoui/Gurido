using UnityEngine;
using UnityEngine.Networking;


public struct CollectibleCurve
{
    public Vector3 curvePoint;
    public CollectibleCurve(Vector3 v)
    {
        curvePoint = v;
    }
}
public class SyncListCollectibleCurve : SyncListStruct<CollectibleCurve> { }

public class CollectibleSpawnCurveController : NetworkBehaviour
{
    //Authorize spawn on network
    [SyncVar]
    bool authorizeSpawn = false;
    // object 
    private float speed = 2f;
    private float count = 0f;

    //Bezier curve
    SyncListCollectibleCurve curvePointsStruct = new SyncListCollectibleCurve();
    private Vector3[] point = new Vector3[3];
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector3 distancePoint;
    private Vector2 direction;
    private Vector2 perpendicularDirection;

    private float directionAngle;
    private float curveFactor;

    //Bounce
    private int bounceCount = 0;
    private int nBounce = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Curve"+bounceCount+" "+nBounce);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClient)
            return;

        if (bounceCount >= nBounce || !authorizeSpawn)
            return;   
        MakeParabola();
    }

    public void InitCurve(float _x, float _y)
    {
        float x = Mathf.Clamp(_x, -1f, 1f);
        float y = Mathf.Clamp(_y, -1f, 1f);

        //Init Curve coordinates
        startPoint = transform.position;
        endPoint = transform.position + new Vector3(x,y).normalized;
        endPoint += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0f));

        direction = endPoint - startPoint;

        //Calculate angle direction from startpoint to end point
        directionAngle = -Vector2.SignedAngle(direction, transform.right);
        perpendicularDirection = (Vector2)(Quaternion.Euler(0, 0, directionAngle + 90) * Vector2.right);

        //Reduce the curve factor if sin(angle) gets closer to 1 or -1
        curveFactor = Mathf.Clamp(1 - Mathf.Abs(Mathf.Sin(AngleToRad(directionAngle))), 0f, 0.7f);

        //invert the curve factor when cos(angle) < 0
        if (Mathf.Cos(AngleToRad(directionAngle)) < 0)
            curveFactor = -curveFactor;

        //randomize the number of bounces
        nBounce = Random.Range(2, 3);

        authorizeSpawn = true;

        Debug.Log("Init Curve");
        CreateCurve();
        
    }

    void CreateCurve()
    {
        //Set the midpoint from mid distance start and end, and to end depending on the sin(angle)
        Vector2 midPoint = Vector2.Lerp(startPoint, endPoint, Mathf.Clamp(curveFactor, 0.5f, 1f));
        midPoint += perpendicularDirection * curveFactor;

        //Create bezier curve points.
        /*
        curvePointsStruct.Add(new CollectibleCurve(startPoint));
        curvePointsStruct.Add(new CollectibleCurve(midPoint));
        curvePointsStruct.Add(new CollectibleCurve(endPoint));
        */
        //point = new Vector3[3];
        point[0] = startPoint;
        point[1] = midPoint;
        point[2] = endPoint;
        
        count = 0f;
    }

    void RemakeCurve()
    {
        //iterate the number of bounces
        bounceCount += 1;
        //set the new points coordinates. The curve will be half the previous one.
        startPoint = endPoint;
        direction /= 2;
        endPoint = startPoint + direction;
        curveFactor /= 2;

        //add a little speed to the next bounce.
        speed += 1f;

        //Recalculate curve
        CreateCurve();
    }

    void MakeParabola()
    {
        if (count < 1.0f)
        {
            count += 1.0f *speed *  Time.deltaTime;

            Debug.Log(point.Length);
            Vector3 m1 = Vector3.Lerp(point[0], point[1], count);
            Vector3 m2 = Vector3.Lerp(point[1], point[2], count);

            //Vector3 m1 = Vector3.Lerp(curvePointsStruct[0].curvePoint, curvePointsStruct[1].curvePoint, count);
            //Vector3 m2 = Vector3.Lerp(curvePointsStruct[1].curvePoint, curvePointsStruct[2].curvePoint, count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        //if the object reaches the end point, call a bounce.
        if (endPoint == new Vector2(transform.position.x, transform.position.y))
            RemakeCurve();
    }

    float AngleToRad(float angle)
    {
        return Mathf.PI / 180 * angle;
    }
}
