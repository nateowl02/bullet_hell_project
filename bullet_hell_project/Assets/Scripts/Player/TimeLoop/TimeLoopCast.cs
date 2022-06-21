using UnityEngine;

public class TimeLoopCast : MonoBehaviour
{
    public TimeLoopPortal timeLoopPortal;

    TimeLoopPortal timePortalLocation = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timePortalLocation == null)
            {
                timePortalLocation = Instantiate(timeLoopPortal, transform.position, Quaternion.identity);
            }
            else
            {
                transform.position = timePortalLocation.transform.position;
                Destroy(timePortalLocation.gameObject);
            }
        }
    }
}
