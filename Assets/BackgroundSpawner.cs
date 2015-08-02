using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject BackgroundPrefab;

	void Start ()
	{
	    var sprite = Resources.Load<Sprite>("Backgrounds/Level1");
	    var background = Instantiate(BackgroundPrefab);
	    background.GetComponent<SpriteRenderer>().sprite = sprite;
	}

    void Update ()
	{
	
	}
}