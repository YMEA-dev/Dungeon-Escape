using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    public void Open()
    {
        if (!open)
        {
            gameObject.SetActive(true);
            open = true;
        }
    }

    public void Close()
    {
        if (open)
        {
            gameObject.SetActive(false);
            open = false;
        }
    }
    
}
