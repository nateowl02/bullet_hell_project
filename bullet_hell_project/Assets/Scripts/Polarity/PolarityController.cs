using UnityEngine;

[RequireComponent(typeof(PolaritySystem))]
public class PolarityController : MonoBehaviour
{
    PolaritySystem polaritySystem;
    private void Start()
    {
        polaritySystem = GetComponent<PolaritySystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            polaritySystem.SwitchPolarity();
        }
    }
}
