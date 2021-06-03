using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static Vector3 GetMousePosition() => GetMousePosition(Camera.main);
    public static Vector3 GetMousePosition(Camera camera)
    {
        var vmouse = Input.mousePosition;
        vmouse.z = Camera.main.transform.position.z;
        return camera.ScreenToWorldPoint(vmouse);
    }
}
