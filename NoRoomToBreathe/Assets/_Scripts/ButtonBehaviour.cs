using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{

    public void Pressed(int n)
    {
        Camera.main.GetComponent<StoryManager>().OptionSelected(n);
    }



}
