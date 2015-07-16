using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public GameObject FocusPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FocusPoint.transform.position;
    }
}
