using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float Delay = 1;

	void Start ()
    {
        Destroy(gameObject, Delay);
	}
}
