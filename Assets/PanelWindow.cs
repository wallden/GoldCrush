using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngineInternal;

public class PanelWindow : MonoBehaviour
{
    private bool _isOpen;
	
    public void ToggleWindow()
    {
        _isOpen = !_isOpen;
        GetComponent<Animator>().SetBool("IsOpen", _isOpen);
    }
}
