using UnityEngine;
using UnityEngine.EventSystems;

/*
NOTE: Looks like Unity alwyas reads the mouse input in the canvas (even if it does
not has a Graphic Raycaster component), so when clicking on the screen, the current object, if it's not under
the clicking point, get's deselected. I found this hacky but efficient way of return
its focus, altough you can 'feel' a small delay when this happens.
For my 'retro' game that simulates a terminal, this comes handy.
*/
public class IgnoreMouseInput : MonoBehaviour
{
    private GameObject lastSelect;

    void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (lastSelect != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelect);
            }
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
