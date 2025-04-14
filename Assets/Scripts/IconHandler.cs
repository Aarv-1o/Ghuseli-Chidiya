using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] _icons; // Create a private Image array variable called _icons and SerializeField it
    [SerializeField] private Color _usedColor; // Create a private Color variable called _usedColor and SerializeField it

    public void UseShot(int shotumber){
        for(int i=0;i<_icons.Length;i++){ // Create a for loop that runs through the _icons array
            if(shotumber==i+1){ // Check if the index of the array is equal to the shownumber
                _icons[i].color=_usedColor; // Change the color of the icon to the _usedColor
                return; // Exit the loop
            }
        }
    }
}
