using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMoveCurve : MonoBehaviour
{
    float speed = 1.5f;
    float count = 0f;
    private Vector3[] point;

    Vector3 distancePoint;
    float directionAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        //Start Init tests

        Vector2 startPoint = transform.position;
        Vector2 endPoint = transform.position + new Vector3(Random.Range(-1f,1f), Random.Range(-0.8f,0.8f)).normalized * Random.Range(0.8f, 1.5f);
        Vector2 dirPoint = endPoint - startPoint ;

        float angle = -Vector2.SignedAngle(dirPoint, transform.right);
        float factor = Mathf.Clamp(1 - Mathf.Abs(Mathf.Sin(AngleToRad(angle))), 0f, 0.7f);

        Vector2 midPoint = Vector2.Lerp(startPoint, endPoint, Mathf.Clamp(factor,0.5f,0.8f));
        Transform t;
        GameObject go = new GameObject();
        t = go.transform;
        Destroy(go);
        t.position = midPoint;

        

        if (Mathf.Cos(AngleToRad(angle)) < 0)
            factor = -factor;

        t.Rotate(Vector3.forward, angle);
        midPoint = t.position + t.up * factor;
        
        //End Init tests

        point = new Vector3[3];
        point[0] = startPoint;
        point[1] = midPoint;
        point[2] = endPoint;
    }

    // Update is called once per frame
    void Update()
    {
        MakeParabola();
    }

    void MakeParabola()
    {
        if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(point[0], point[1], count);
            Vector3 m2 = Vector3.Lerp(point[1], point[2], count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
    }

    float AngleToRad(float angle)
    {
        return Mathf.PI / 180 * angle;
    }
}
