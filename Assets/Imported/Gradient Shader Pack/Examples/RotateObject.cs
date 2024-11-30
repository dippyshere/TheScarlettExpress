#region

using UnityEngine;

#endregion

public class RotateObject : MonoBehaviour
{
    public float x;
    public float y;
    public float z = 40;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
    }
}