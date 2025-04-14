using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshothandler : MonoBehaviour // Create a class called Slingshothandler that inherits from MonoBehaviour 
//MonoBehaviour is the base class from which every Unity script derives. It provides the basic framework for building scripts in Unity.
{   
    // SerializeField is a Unity attribute that allows you to expose a private variable in the Inspector.
    // This is useful when you want to expose a variable to the Unity Editor but you don't want to make it public.
    // Header is a Unity attribute that allows you to add a header above a variable in the Inspector.
    [SerializeField]private LineRenderer _leftLineRenderer; // Create a private LineRenderer variable called _leftLineRenderer
    [SerializeField]private LineRenderer _rightLineRenderer; // Create a private LineRenderer variable called _rightLineRenderer

    [SerializeField] private Transform _leftstartposition; // Create a private Transform variable called _leftstartposition and SerializeField it
    [SerializeField] private Transform _rightstartposition; // Create a private Transform variable called _rightstartposition and SerializeField it
    [SerializeField] private Transform _centreposition; 
    [SerializeField] private Transform _idealposition;
    private Vector2 _slinghshotlinesposition; // To clamp the slingshot line position
    [SerializeField]private float _maxdist=3.5f; // To set the maximum distance of the slingshot
    [SerializeField] private float _ShotForce=10f; // To set the minimum distance of the slingshot

    [SerializeField] private float _timeBetweenBirds=2f;

    [SerializeField] private SlingshotArea _slingshotarea; // Create a private SlingshotArea variable called _slingshotarea to help in calling the script
    private bool _clickedwithinarea; // To call the SlinghshotArea script and check if the mouse is within the slingshot area fuction

    [SerializeField] private AngieBird _angiebirdPrefab; // Create a private GameObject variable called _angiebirdPrefab and SerializeField it
    [SerializeField] private float _angiebirdPositionOffset=2f;
    
    private AngieBird _spwanedangiebird; // Create a private GameObject variable called _angiebird
    private Vector2 direction;
    private Vector2 directionNormalized;
    
    private bool _birdonslingshot;
    private void Awake(){
        _leftLineRenderer.enabled=false; // To Disappaer Left Line Renderer
        _rightLineRenderer.enabled=false; // TO Disappaer Right Line Renderer
        SpawnAngieBird(); // Call the SpawnAngieBird function
    }
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && _slingshotarea.iswithinslinghshotarea()) // Check if the left mouse button was pressed and if the mouse is within the slingshot area
        {
            _clickedwithinarea=true; // Set the _clickedwithinarea to true
        }
        if(Mouse.current.leftButton.isPressed && _clickedwithinarea && _birdonslingshot) // Check if the left mouse button is pressed and if the mouse is within the slingshot area
        {
           DrawSlingshotLine(); // Call the DrawSlingshotLine function
           PositionAndRotateAngrybird();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame &&_birdonslingshot) // Check if the left mouse button was released and if the mouse is within the slingshot area
        {
           
            if(GameManager.instance.enoughshots()){
                _spwanedangiebird.LaunchBird(directionNormalized,_ShotForce);
                GameManager.instance.UsedShots();
                _birdonslingshot=false;
                _leftLineRenderer.enabled=false;
                _rightLineRenderer.enabled=false;
                
            }
            if(GameManager.instance.enoughshots()){
                StartCoroutine(SpawnAngieBirdAfterTime());
            };
        }
    }

    private void DrawSlingshotLine(){
        Vector3 touchposition=Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // To Get the mouse position using built-in methods
        _slinghshotlinesposition=Vector3.ClampMagnitude(touchposition-_centreposition.position,_maxdist)+_centreposition.position; // To clamp the slingshot line position
        SetLines(_slinghshotlinesposition); // Call the SetLines function and uses the touchposition as a parameter
        direction= (Vector2)_centreposition.position-_slinghshotlinesposition;// To get the direction of the slingshot and change it to Vector2
        directionNormalized=direction.normalized; // To get the normalized direction of the slingshot

    }
    private void SetLines(Vector2 position){ // Create a function called SetLines that takes a Vector2 parameter called position for using touch position
        if(!_leftLineRenderer.enabled && !_rightLineRenderer.enabled){ // Check if the left and right line renderer is not enabled
            _leftLineRenderer.enabled=true; // Enable the left line renderer
            _rightLineRenderer.enabled=true; // Enable the right line renderer
        }
        
        _leftLineRenderer.SetPosition(0,position); // Set the first position of the left line renderer to the position
        _leftLineRenderer.SetPosition(1,_leftstartposition.position); // Set the second position of the left line renderer to the _leftstartposition position
        _rightLineRenderer.SetPosition(0,position); // Set the first position of the right line renderer to the position
        _rightLineRenderer.SetPosition(1,_rightstartposition.position); // Set the second position of the right line renderer to the _rightstartposition position  
    }

    private void SpawnAngieBird(){
        SetLines(_idealposition.position); // Call the SetLines function and uses the _idealposition as a parameter to spawn angie bird in Idealposition
        
        Vector2 dir=(_centreposition.position-_idealposition.position).normalized; // To get the direction of the slingshot and change it to Vector2
        Vector2 spawnposition=(Vector2)_idealposition.position+dir*_angiebirdPositionOffset; // To get the spawn position of the angie bird
        _spwanedangiebird=Instantiate(_angiebirdPrefab,spawnposition,Quaternion.identity); // Instantiate the _angiebirdPrefab at the _idealposition position helps to spawn the angie bird
        // _spwanedangiebird.transform.right=dir; // To get the direction of the angie bird
       
       _birdonslingshot=true;
    }

    private void PositionAndRotateAngrybird(){
        _spwanedangiebird.transform.position=_slinghshotlinesposition;
        _spwanedangiebird.transform.right=directionNormalized;

    }
    private IEnumerator SpawnAngieBirdAfterTime(){
        yield return new WaitForSeconds(_timeBetweenBirds);
        SpawnAngieBird();
    }










}   
