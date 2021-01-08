using UnityEngine;

public class Rotator : MonoBehaviour
{

    private int rotationSpeed = 45;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
