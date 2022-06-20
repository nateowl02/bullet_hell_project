using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public Transform targetObject;
    public Transform[] orbitters;
    public float startSpeed;
    public float endSpeed;
    public float startRadius;
    public float endRadius;
    public float frequency;


    float currentSpeed;
    float currentRadius;

    void Start()
    {
        currentSpeed = startSpeed;
        float temp_angle = 360 / orbitters.Length;
        float currentAngle = 0;
        for (int i = 0; i < orbitters.Length; i++)
        {
            orbitters[i].transform.position = targetObject.transform.position + new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0).normalized * startRadius;
            currentAngle += temp_angle;

        }

    }

    void FixedUpdate()
    {
        currentSpeed = Mathf.Lerp(startSpeed, endSpeed, Mathf.Abs(Mathf.Sin(Time.time)) * frequency); 
        currentRadius = Mathf.Lerp(startRadius, endRadius, Mathf.Abs(Mathf.Sin(Time.time)) * frequency);
        for (int i = 0; i < orbitters.Length; i++)
        {
            orbitters[i].transform.position = (orbitters[i].transform.position - targetObject.transform.position).normalized * currentRadius + targetObject.position;
            orbitters[i].RotateAround(targetObject.transform.position, Vector3.forward, currentSpeed  * Time.deltaTime);
        }
        
    }

}
