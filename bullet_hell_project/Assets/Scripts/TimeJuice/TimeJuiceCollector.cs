using UnityEngine;

public class TimeJuiceCollector : MonoBehaviour
{
    public string collectionTag = "TimeJuice";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == collectionTag)
        {
            Destroy(other.gameObject);
        }
    }
}
