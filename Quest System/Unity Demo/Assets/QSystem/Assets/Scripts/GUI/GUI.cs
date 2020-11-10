using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: GUI.Instance of QGUI...Instance kan niet aangeroepen worden 
public class GUI<T> : Manager<T> where T : GUI<T>
{
    public bool hideOnAwake = false;

    public override void Awake()
    {
        gameObject.SetActive(!hideOnAwake);
    }
}
