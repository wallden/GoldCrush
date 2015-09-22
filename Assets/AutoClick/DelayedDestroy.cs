using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float Delay = 1;

	void Start ()
    {
        Object.Destroy(gameObject, Delay);
	}
}
