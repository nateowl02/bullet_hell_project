using UnityEngine;

public class TImeJuice : MonoBehaviour
{
    public float speed = 0.2f;
    
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
