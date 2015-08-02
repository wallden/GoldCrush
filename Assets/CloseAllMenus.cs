using UnityEngine;
using System.Collections;

public class CloseAllMenus : MonoBehaviour {

    public RectTransform InGameStoreRectTransform;
    private RectTransform LocalRect { get; set; }
	// Use this for initialization
	void Start ()
	{
        LocalRect = GetComponent<RectTransform>();
        LocalRect.position = new Vector3(Screen.width/2f,Screen.height);
    }

   

    // Update is called once per frame
	void Update () {
	   LocalRect.sizeDelta = new Vector3(Screen.width, (Screen.height/1.1f));
	}
}
