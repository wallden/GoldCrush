using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
    void Update ()
    {
        var mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseInWorld.x, mouseInWorld.y, transform.position.z);
    }
}