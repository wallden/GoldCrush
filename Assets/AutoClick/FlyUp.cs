using UnityEngine;

public class FlyUp : MonoBehaviour
{
    public float FlySpeed = 1;

	public void Update ()
	{
	    transform.position += Vector3.up*FlySpeed*Time.deltaTime;
	}
}
