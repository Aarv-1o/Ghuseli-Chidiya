using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SlingshotArea : MonoBehaviour
{   
    [SerializeField]LayerMask _slingshotareamask;
    public bool iswithinslinghshotarea(){
        Vector2 worldposition=Camera.main.ScreenToWorldPoint(Input.mousePosition);//Reads the mouse position 
        if(Physics2D.OverlapPoint(worldposition,_slingshotareamask)){//check if the mouse position is within the collider
            return true;
        }
        else{
            return false;
        }

    }
    
}
